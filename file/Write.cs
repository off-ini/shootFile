using System;
using System.IO;

namespace ShootFile
{

    
    class Write
    {
        String msg;
       FileStream fos {get; set;}
       String TargetFile {get; set;}
       int intbuffer {get; set;}  //8388608;//5242880;
       private byte[] b;

       public Write(String TargetFile)
       {
           this.TargetFile = TargetFile;
           fos = new FileStream(this.TargetFile, FileMode.Create, FileAccess.ReadWrite);
           this.intbuffer = 1024;
           b = new byte[this.intbuffer];
       }

       public void setIntbuffer(int intbuffer)
       {
           this.intbuffer = intbuffer;
           this.b = new byte[intbuffer];
       }

       public bool writeByte(int o)
       {
           try{
               this.fos.Write(this.b, 0, o);
               return true;
           }catch(Exception e)
           {
               this.msg = e.StackTrace;
               return false;
           }
       }

       public bool writeByte(byte[] o)
       {
           try{
               this.fos.Write(o, 0, this.intbuffer);
               return true;
           }catch(Exception e)
           {
               this.msg = e.StackTrace;
               return false;
           }
       }
       
       public void close()
       {
            if (this.fos != null)
            {
                this.fos.Close();
            }
       }
    
    }


}