using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogAndCatBot.Models
{
    public class DogAPIAnswer
    {
        private string message;

        private string status;


        public string Message
        {
            get => message;
            set
            {
                message = value;
            }
        }


        public string Status
        {
            get => status;
            set
            {
                status = value;
            }
        }
    }
}
