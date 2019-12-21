using System;  
using System.Net;  
using System.Net.Sockets;  
using System.Text;  

namespace ShootFile
{

// Socket Listener acts as a server and listens to the incoming   
// messages on the specified port and protocol.  
public class Server  
{  
   String msg;
   Socket listener;
   Socket handler;
   Write fos;
   bool status = false;
   public Server(String ip, int port){
       this.init(ip, port);
   }

   public Server(String ip)
   {
       this.init(ip, 2699);
   }

   public Server()
   {
       this.init("127.0.0.1", 2699);
   }

   private void init(String ip, int port)
   {
       IPAddress ipAddress = IPAddress.Parse(ip);
       IPEndPoint localEndPoint = new IPEndPoint(ipAddress, port);
       try{
           this.listener = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
           this.listener.Bind(localEndPoint);
           this.listener.Listen(10);
           starting();
       }catch(Exception e)
       {
           this.msg = e.Message;
       }
   }
    
    private void starting()
    {
        try{
            Console.WriteLine("[SERVER]  Attent de connexion...");  
            this.handler = listener.Accept(); 
            Console.WriteLine("[SERVER]  Connecté");
        }catch(Exception e)
        {
            this.msg = e.Message;
        } 
    }

    public void file(String f)
    {
        int index = f.LastIndexOf("/") > -1 ? f.LastIndexOf("/") : f.LastIndexOf("\\");
        String name = null;
        if(index > -1)
        {
            name = f.Substring(index + 1);
        }else name = f;
        this.fos = new Write(name);
        //this.fos = new Write(name);
    }

    public void writeFile()
    {
        try{
            string data = null;  
            byte[] bytes = null;
            byte[] msg = null;  
            int k = 0, l = 0;
            while (true)  
            {  
                bytes = new byte[1024];  
                int bytesRec = handler.Receive(bytes);  
                data = Encoding.ASCII.GetString(bytes, 0, bytesRec);

                if(this.status)
                {
                    if (data.IndexOf("<EOF>") == -1)  
                    {  
                       /* j++;
                        int d = BitConverter.ToInt32(bytes, 0);
                        Console.WriteLine(j + " : " +d);*/
                        
                        this.fos.writeByte(bytes);
                        msg = Encoding.ASCII.GetBytes("1");  
                        handler.Send(msg);
                    }else{
                        this.status = false;
                        break;
                    }
                }else
                {
                    if(data.IndexOf("<BOF>") > -1)
                    {
                        String n = data.Substring(data.IndexOf("<BOF>")+5);
                        this.file(n);
                        this.status = true;
                        Console.Write("[SERVER]  File: {0}   ", n);
                    } 
                }
                
                //Console.WriteLine("Text received : {0}", data);
                k++; l++;
                if(k > 3) k = 1;
                if(l > 2) l = 1;

                if(l == 1)
                switch(k)
                {
                    case 1:
                        Console.Write("\\");
                    break;
                    case 2:
                        Console.Write("/");
                    break;
                    case 3:
                        Console.Write("-");
                    break;
                }
                else Console.Write("\b");
            } 

            fos.close();
            Console.WriteLine("   -- fini --\n");
        }catch(Exception e)
        {
            this.msg = e.Message;
        }
    }

    public void close()
    {
        handler.Shutdown(SocketShutdown.Both);  
        handler.Close();
    }
    public void StartServer()  
    {  
        // Get Host IP Address that is used to establish a connection  
        // In this case, we get one IP address of localhost that is IP : 127.0.0.1  
        // If a host has multiple addresses, you will get a list of addresses  
        IPHostEntry host = Dns.GetHostEntry("localhost");  
        IPAddress ipAddress = host.AddressList[0];  
        IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 11000);    
        
  
        try {   
  
            // Create a Socket that will use Tcp protocol      
            Socket listener = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);  
            // A Socket must be associated with an endpoint using the Bind method  
            listener.Bind(localEndPoint);  
            // Specify how many requests a Socket can listen before it gives Server busy response.  
            // We will listen 10 requests at a time  
            listener.Listen(10);  
  
            Console.WriteLine("Waiting for a connection...");  
            Socket handler = listener.Accept();  
  
             // Incoming data from the client.    
            string data = null;  
            byte[] bytes = null;
            byte[] msg = null;  
            Write fos = new Write("copy/char.png");
            int j = 0;
            while (true)  
            {  
                bytes = new byte[1024];  
                int bytesRec = handler.Receive(bytes);  
                data = Encoding.ASCII.GetString(bytes, 0, bytesRec);  
                if (data.IndexOf("<EOF>") == -1)  
                {  
                    j++;
                    int d = BitConverter.ToInt32(bytes, 0);
                    Console.WriteLine(j + " : " +d);
                    
                     fos.writeByte(bytes);
                    msg = Encoding.ASCII.GetBytes(data);  
                    //handler.Send(bytes);
                }else{
                    break;
                }
                //Console.WriteLine("Text received : {0}", data);
            } 

            fos.close(); 
  
            Console.WriteLine("Text received : {0}", data);  
  
            msg = Encoding.ASCII.GetBytes(data);  
            handler.Send(msg);  
            handler.Shutdown(SocketShutdown.Both);  
            handler.Close();  
        }  
        catch (Exception e)  
        {  
            Console.WriteLine(e.ToString());  
        }  
  
        Console.WriteLine("\n Press any key to continue...");  
        Console.ReadKey();  
    }          
} 

}
