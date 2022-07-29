using System;
namespace Project2.Models
{
	public class Error
	{
		public String msg { get; set; }
		public int status { get; set; }

        public Error(string msg, int status)
        {
            this.msg = msg;
            this.status = status;
        }

        public Error()
		{
		}
	}
}

