using System.Windows;

namespace E_Mail_Address_Book
{
    /// <summary>
    /// Interaction logic for details.xaml
    /// </summary>
    public partial class details : Window
    {
        public Person personInfo { get; set; } // class Person constr in pernson.cs

        public details(Person p) //pass the elements
        {
            InitializeComponent();
            personInfo = p;  
            DataContext = p;
        }
    }
}
