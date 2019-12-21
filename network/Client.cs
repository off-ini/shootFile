using System;  
using System.Net;  
using System.Net.Sockets;  
using System.Text; 

namespace ShootFile
{
// Client app is the one sending messages to a Server/listener.   
// Both listener and client can send messages back and forth once a   
// communication is established.  
public class Client  
{  
    byte[] bytes = new byte[1024]; 
    String msg;
    Socket sender;
    Read fis;
   public Client(String ip, int port){
       this.init(ip, port);
   }

   public Client(String ip)
   {
       this.init(ip, 2699);
   }

   public Client()
   {
       this.init("127.0.0.1", 2699);
   }
  
    private void init(String ip, int port)
    {
        IPAddress ipAddress = IPAddress.Parse(ip);
        IPEndPoint remoteEP = new IPEndPoint(ipAddress, port);
        try{
            this.sender = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            sender.Connect(remoteEP);
            Console.WriteLine("[CLIENT] Connecté a {0}",  sender.RemoteEndPoint.ToString()); 
        }catch (SocketException se)  
        {  
            msg = se.Message;  
        }  
        catch (Exception e)  
        {  
            msg = msg = e.Message; 
        }  
    }

    public void sendFile(String name)
    {
        this.fis = new Read(name);
        try{
                int bytesRec;
                int bytesSent;

                int i, j = 0;
                int k = 0, l = 0;
                //byte[] msg = Encoding.ASCII.GetBytes("This is a test<EOF>");0
            
                sender.Send(Encoding.ASCII.GetBytes("<BOF>"+name));
                Console.Write("[SENDING]    ");
                while ((i = fis.readByte(out bytes)) > 0)
                {
                    j++;
                    //Console.WriteLine("[CLIENT] {0}  :  {1}", j, i);
                    bytesSent = sender.Send(bytes);
                    bytesRec = sender.Receive(new Byte[0]);
                   // Console.WriteLine("[SERVER] {0}", Encoding.ASCII.GetString(bytes, 0, bytesRec));
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

                sender.Send(Encoding.ASCII.GetBytes("<EOF>"));
  
                // Send the data through the socket.    
                //int bytesSent = sender.Send(msg);  
                   
  
                // Receive the response from the remote device.    
                bytesRec = sender.Receive(bytes);  
                /*Console.WriteLine("Echoed test = {0}",  
                    Encoding.ASCII.GetString(bytes, 0, bytesRec));*/
                      
                fis.close();
                Console.WriteLine("   -- fini --\n");
        }catch (ArgumentNullException ane)  
        {  
            msg = ane.Message;  
        }  
        catch (Exception e)  
        {  
            msg = msg = e.Message; 
        }  
    }

    public void close()
    {
        sender.Shutdown(SocketShutdown.Both);  
        sender.Close(); 
    }
    public void StartClient()  
    {  
        byte[] bytes = new byte[1024];  
  
        try  
        {  
            // Connect to a Remote server  
            // Get Host IP Address that is used to establish a connection  
            // In this case, we get one IP address of localhost that is IP : 127.0.0.1  
            // If a host has multiple addresses, you will get a list of addresses  
            IPHostEntry host = Dns.GetHostEntry("localhost");  
            IPAddress ipAddress = IPAddress.Parse("127.0.0.1"); //host.AddressList[0];  
            IPEndPoint remoteEP = new IPEndPoint(ipAddress, 2699);  
  
            // Create a TCP/IP  socket.    
            Socket sender = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);  
  
            // Connect the socket to the remote endpoint. Catch any errors.    
            try  
            {  
                // Connect to Remote EndPoint  
                sender.Connect(remoteEP);  
  
                Console.WriteLine("Socket connected to {0}",  sender.RemoteEndPoint.ToString());  
  
                // Encode the data string into a byte array.    
                //byte[] msg = Encoding.ASCII.GetBytes("This is a test<EOF>"); 
                
                int bytesRec;
                int bytesSent;

                int i, j = 0;
                //byte[] msg = Encoding.ASCII.GetBytes("This is a test<EOF>");0
                String name = "image.png";
                Read fis = new Read("image.png");
                
                sender.Send(Encoding.ASCII.GetBytes("<BOF>"+name));
                while ((i = fis.readByte(out bytes)) > 0)
                {
                    j++;
                    Console.WriteLine("[CLIENT] {0}  :  {1}", j, i);
                    bytesSent = sender.Send(bytes);
                    bytesRec = sender.Receive(new Byte[0]);
                    //Console.WriteLine("[SERVER] {0}", Encoding.ASCII.GetString(bytes, 0, bytesRec));
                }

                sender.Send(Encoding.ASCII.GetBytes("<EOF>"));
  
                // Send the data through the socket.    
                //int bytesSent = sender.Send(msg);  
                   
  
                // Receive the response from the remote device.    
                bytesRec = sender.Receive(bytes);  
                Console.WriteLine("Echoed test = {0}",  
                    Encoding.ASCII.GetString(bytes, 0, bytesRec));
                      
                fis.close();
                // Release the socket.    
                sender.Shutdown(SocketShutdown.Both);  
                sender.Close();  
  
            }  
            catch (ArgumentNullException ane)  
            {  
                Console.WriteLine("ArgumentNullException : {0}", ane.ToString());  
            }  
            catch (SocketException se)  
            {  
                Console.WriteLine("SocketException : {0}", se.ToString());  
            }  
            catch (Exception e)  
            {  
                Console.WriteLine("Unexpected exception : {0}", e.ToString());  
            }  
  
        }  
        catch (Exception e)  
        {  
            Console.WriteLine(e.ToString());  
        }  
    }  
} 

}
