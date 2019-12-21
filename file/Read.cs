using System;
using System.IO;

namespace ShootFile
{
    

    class Read
    {
        String msg;
       FileStream fis {get; set;}
       String SourceFil {get; set;}
       int intbuffer {get; set;}  //8388608;//5242880;
       private byte[] b;

       public Read(String SourceFil)
       {
           this.SourceFil = SourceFil;
           this.fis = new FileStream(this.SourceFil, FileMode.Open, FileAccess.ReadWrite);
           this.intbuffer = 1024;
           b = new byte[this.intbuffer];
       }

      public void setIntbuffer(int intbuffer)
       {
           this.intbuffer = intbuffer;
           this.b = new byte[intbuffer];
       }

       public int readByte(out byte[] bit)
       {
           bit = new byte[this.intbuffer];
           try{
               return this.fis.Read(bit, 0, this.intbuffer);
           }catch(Exception e)
           {
               this.msg = e.StackTrace;
               return -1;
           }
       }

       public void close()
       {
            if (this.fis != null)
            {
                this.fis.Close();
            }
       }
       
    }


}