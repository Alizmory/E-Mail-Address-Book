using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;

namespace E_Mail_Address_Book
{
    public class VM : INotifyPropertyChanged
    {
        private static VM vm;
        private const string filepath = "/res/contacts.txt";
        public ObservableCollection<Person> Contacts { get; set; } = new ObservableCollection<Person>();

        public static VM Instance
        {
            get
            {
                vm ??= new VM();
                return vm;
            }
        }
        private Person selectedName;
        public Person SelectedName
        {
            get { return selectedName; }
            set { selectedName = value; propChange(); }
        }
        private string errorMessage; //just for the try-catch part, actually no display

        public void Init()
        {
            Contacts.Clear();
            string[] Persons_List = File.ReadAllLines(filepath);
            foreach (string person in Persons_List)
            { 
                string[] props = person.Split(new char[] { ' ' });
                //actually, here will not read the blanked line
                //splite by space, but blanket line not contain space
                try
                {
                    Contacts.Add(new Person
                    {
                        Name = props[0].Trim(),
                        Email = props[1].Trim(),
                        Phone = props[2].Trim()
                        //use Trim() in case someone error input muti-space
                        //such as: x  x (two space inside)
                    }
                            );
                }
                catch (Exception e) 
                    //if use foreach, don't need try catch.
                    //i just laze, use catch to stop loop. otherwise it will be out of index
                {
                    errorMessage = e.Message;
                }
            }
        }
        #region 
        public event PropertyChangedEventHandler PropertyChanged;
        private void propChange([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
