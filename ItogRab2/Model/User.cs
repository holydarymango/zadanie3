using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItogRab2.Model
{
    public class User
    {
        public string InputText { get; set; }
        public bool IsValid { get; set; } 

        public User() { }  
        public User(string inputText)
        {
            InputText = inputText;
            IsValid = !string.IsNullOrEmpty(inputText);
        }

        public void Validate()
        {
            IsValid = !string.IsNullOrEmpty(InputText) && InputText.Length > 3;
        }
    }
}
