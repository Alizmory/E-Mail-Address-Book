using System;
using System.Collections.Generic;
using System.Windows;

/*
for presentation logic notes:
1. create contacts.txt: 5 person and put into the "c:\res\" folder, data formating such as:
        LzGuo 123-123-1234 dd@8010.ca   (splite: space) //actually, i want to try the regex expression but i have not idea yet.
2. click projection --> add windows, name it "details"
3. create two xaml
   -- main windows: put a list which only display name: bind person[name]  //mainwindow.xaml.cs, and dataview part is vm.cs
   -- detail windows: bind with person[name],[phone],[email] //details.xmal.cs
4.  i use person.cs to create data model 
    in the person.cs,  create a class call Person to {get/set} the datas
5. in vm.cs, create a char[] to instore the data read from txt
6. details.cs. create new instance of Person call "p". and datacontext=p, because the details.xaml need. 
*/



namespace E_Mail_Address_Book
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        readonly VM vm;
        readonly Dictionary<string, details> _activeDisplay = new Dictionary<string, details>();  
        //create dictionary for detail windows datas
        public MainWindow()
        {
            InitializeComponent();
            vm = VM.Instance;
            DataContext = vm;
            vm.Init();
        }
        private void ListBox_SelectionChanged(object sender, RoutedEventArgs e) //when click item in main window, display detail in detai window
        {
            if (vm.SelectedName != null)
            {
                if (_activeDisplay.TryGetValue(vm.SelectedName.Email, out details window))
                {
                    window.Focus();//focus the details window
                }
                else
                {
                    details dw = new details(vm.SelectedName) { Owner = this }; 
                    dw.Closed += dw_Closed; 
                    dw.Show(); 
                    _activeDisplay.Add(vm.SelectedName.Email, dw);
                    //i don't want to restart mainwindow again.
                    //reference: Rick's book p125
                }
            }
        }
        private void dw_Closed(object sender, EventArgs e)
        {  //return to null
            details dw = sender as details;
            dw = null;
        }
    }
}
