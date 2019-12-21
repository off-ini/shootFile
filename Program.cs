using System;
using System.IO;

namespace ShootFile
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            int choix = menu();
            String host = null, file = null;
            Boolean stat = true;
            ConsoleKeyInfo s = new ConsoleKeyInfo();

            do{
                if(choix == 1)
                {
                    Boolean Ss = true;
                    Console.Clear();
                    heading("  SERVER  ");
                    Console.Write("[HOST]  ");
                    host = Console.ReadLine();
                    Server sc = new Server(host);
                    do{
                        sc.writeFile();
                        /*Console.Write("\n\tAppuiez sur [ BACKSPACE ] pour Quitter ce mode et une autre [ Touche ] pour Continue");
                        s = Console.ReadKey(true);
                        if (s.Key == ConsoleKey.Backspace) Ss = false;*/
                    }while(Ss);
                   
                    sc.close();
                    Console.Clear();
                }else if (choix == 2)
                {
                    Boolean Sc = true;
                    Console.Clear();
                    heading("  CLIENT  ");
                    Console.Write("[HOST]  ");
                    host = Console.ReadLine();
                
                    Client cl = new Client(host);
                    do{
                        Console.Write("[FILE]  ");
                        file = Console.ReadLine();
                        cl.sendFile(file);
                        Console.Write("\n\tAppuiez sur [ BACKSPACE ] pour Quitter ce mode et une autre [ Touche ] pour Continue\n");
                        s = Console.ReadKey(true);
                        if (s.Key == ConsoleKey.Backspace) Sc = false;
                    }while(Sc);
                   
                    cl.close();
                    Console.Clear();
                }else{
                    Console.WriteLine("\n\n\t  Mercie d'avoir utliser l'application");
                    Console.WriteLine("      *********************************************\n\n");
                    stat = false;
                }
            }while(stat);

           Console.WriteLine("[Fin]");
           Console.ReadLine();
                        
        }

        static int menu()
        {
            int choix = 1;
            do
            {
                Console.WriteLine("\t*************************************************************************");
                Console.WriteLine("\t*****************                                       *****************");
                Console.WriteLine("\t*****************              SHOOT FILE               *****************");
                Console.WriteLine("\t*****************                                       *****************");
                Console.WriteLine("\t*************************************************************************");

                Console.WriteLine("\t\t  1 -  Server");
                Console.WriteLine("\t\t  2 -  Client");
                Console.WriteLine("\t\t  3 -  Quitter\n");

                if (choix < 1 || choix > 3) Console.WriteLine("\n\t\t [ --- Choix incorrete --- ]");  
                do
                {
                    Console.Write("\t  Choix   >>  ");
                } while (!int.TryParse(Console.ReadLine(), out choix));
                if (choix < 1 || choix > 3) Console.Clear();
                else return choix;
            } while (choix < 1 || choix > 3);
            return 0;
        }

        static void heading(String head)
        {
            Console.WriteLine("\t*************************************************************************");
            Console.WriteLine("\t*****************              {0}               *****************", head);
            Console.WriteLine("\t*************************************************************************");
            Console.WriteLine("\n\n");
        }


       /* public static void CopyByte(String SourceFile, String TargetFile)
        {

            Read fis = new Read(SourceFile);
            Write fos = new Write(TargetFile);

                try
                {
                    Console.Write("## Try No.  : (Write from " + SourceFile + " to " + TargetFile + ")\n");

                    int i;
                    while ((i = fis.readByte()) > 0)
                    {
                        Console.WriteLine(i);
                        fos.writeByte(1024);
                    }

                    Console.Write("Writing file : " + TargetFile + " is successful.\n");

                    //break;
                }
                catch (Exception e)
                {
                    Console.Write("Writing file : " + TargetFile + " is unsuccessful.\n");
                    Console.Write(e);
                }
                finally
                {
                   fis.close();
                   fos.close();
                }
        }*/

        public static void Copy(String SourceFile, String TargetFile)
        {

            FileStream fis = null;
            FileStream fos = null;

                try
                {
                    Console.Write("## Try No.  : (Write from " + SourceFile + " to " + TargetFile + ")\n");


                    fis = new FileStream(SourceFile, FileMode.Open, FileAccess.ReadWrite);
                    fos = new FileStream(TargetFile, FileMode.Create, FileAccess.ReadWrite);

                    int intbuffer = 8388608;//5242880;
                    byte[] b = new byte[intbuffer];

                    int i;
                    while ((i = fis.Read(b, 0, intbuffer)) > 0)
                    {
                        Console.WriteLine(i);
                        fos.Write(b, 0, i);
                    }

                    Console.Write("Writing file : " + TargetFile + " is successful.\n");

                    //break;
                }
                catch (Exception e)
                {
                    Console.Write("Writing file : " + TargetFile + " is unsuccessful.\n");
                    Console.Write(e);
                }
                finally
                {
                    if (fis != null)
                    {
                        fis.Close();
                    }
                    if (fos != null)
                    {
                        fos.Close();
                    }
                }
        }


    }
}
