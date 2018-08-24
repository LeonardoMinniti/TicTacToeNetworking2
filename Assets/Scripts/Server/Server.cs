using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using UnityEngine;

public class Server : MonoBehaviour
{

    public static string TempData;
    public static bool ServerStarted;
    public static bool IsWaitingForSecondPlayer;
    public int port = 6321;


    private List<ServerClient> clients;
    private List<ServerClient> disconnectList;
    private TcpListener server;
    private bool stop = false;
    private bool sendXOrO;
    private int count;


    private void Update()
    {
        if (!ServerStarted)
            return;
        if (clients.Count <= 1)
        {
            IsWaitingForSecondPlayer = true;
        }
        else
        {
            IsWaitingForSecondPlayer = false;
        }

        foreach (ServerClient c in clients)
        {
            if (!IsConnected(c.tcp))
            {
                c.tcp.Close();
                clients.Remove(c);
                disconnectList.Add(c);
                continue;
            }
            else
            {
                NetworkStream s = c.tcp.GetStream();
                if (s.DataAvailable)
                {
                    StreamReader reader = new StreamReader(s, true);
                    string data = reader.ReadLine();

                    if (data != null)
                        OnIncomingData(c, data);
                }
            }
        }

        for(int i = 0; i < disconnectList.Count - 1; i++)
        {
            Broadcast(disconnectList[i].clientName + "has disconnected", clients);
            clients.Remove(disconnectList[i]);
            disconnectList.RemoveAt(i);
        }

        if (clients.Count < 2)
        {
            sendXOrO = true;
        }

        if(clients.Count == 2 && sendXOrO)
        {
            try
            {
                StreamWriter writer = new StreamWriter(clients[0].tcp.GetStream());
                writer.WriteLine("&X");
                writer.Flush();
            }
            catch(Exception e)
            {
                Debug.Log("Write error : " + e.Message + "to client ");
            }

            try
            {
                StreamWriter writer1 = new StreamWriter(clients[1].tcp.GetStream());
                writer1.WriteLine("&O");
                writer1.Flush();
            }
            catch (Exception e)
            {
                
            }
            sendXOrO = false;
        }
    }

    public void HostServer()
    {
        clients = new List<ServerClient>();
        disconnectList = new List<ServerClient>();

        try
        {
            server = new TcpListener(IPAddress.Any, port);
            server.Start();

            StartListening();
            ServerStarted = true;
            Debug.Log("Server has been started on port " + port.ToString());
        }
        catch (Exception e)
        {
            Debug.Log("Socket error: " + e.Message);
        }
    }

    private void OnIncomingData(ServerClient c, string data)
    {

        if (data.Contains("&NAME"))
        {
            c.clientName = data.Split('|')[1];
            Debug.Log(c.clientName);
            Broadcast(c.clientName + " has connected!", clients);
            return;
        }

        Broadcast(c.clientName + " : " + data, clients);
        Debug.Log(c.clientName + "has sent the following message : " + data);
    }

    private void StartListening()
    {
        server.BeginAcceptTcpClient(AcceptTcpClient, server);
    }

    private bool IsConnected(TcpClient c)
    {
        try
        {
            if (c != null && c.Client != null && c.Client.Connected)
            {
                if (c.Client.Poll(0, SelectMode.SelectRead))
                {
                    return !(c.Client.Receive(new byte[1], SocketFlags.Peek) == 0);
                }

                return true;
            }
            else
                return false;
        }
        catch
        {
            return false;
        }
    }

    private void AcceptTcpClient(IAsyncResult ar)
    {
        TcpListener listener = (TcpListener)ar.AsyncState;

        clients.Add(new ServerClient(listener.EndAcceptTcpClient(ar)));

        StartListening();

        // Send a message to every one
        Broadcast("%NAME", new List<ServerClient>() { clients[clients.Count - 1] });
    }

    private void Broadcast(string data, List<ServerClient> cl)
    {
        foreach(ServerClient c in cl)
        {
            try
            {
                StreamWriter writer = new StreamWriter(c.tcp.GetStream());
                writer.WriteLine(data);
                writer.Flush();
            }
            catch(Exception e)
            {
                Debug.Log("Write error : " + e.Message + "to client " + c.clientName);
            }
        }
    }
}

public class ServerClient
{
    public TcpClient tcp;
    public string clientName;

    public ServerClient(TcpClient clientSocket)
    {
        clientName = "Guest";
        tcp = clientSocket;
    }
}