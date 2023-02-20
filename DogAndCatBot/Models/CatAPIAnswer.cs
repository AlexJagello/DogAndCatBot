using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogAndCatBot.Models
{
    public class CatAPIAnswer
    {
        private string id;

        private string url;

        private int width;

        private int height;


        public string Id
        {
            get => id;
            set
            {
                id = value;
            }
        }

        public string Url
        {
            get => url;
            set
            {
                url = value;
            }
        }

        public int Width
        {
            get => width;
            set
            {
                width = value;
            }
        }

        public int Height
        {
            get => height;
            set
            {
                height = value;
            }
        }
    }
}
