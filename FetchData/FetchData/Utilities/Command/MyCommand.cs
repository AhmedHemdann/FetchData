using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FetchData.Utilities.Command
{
    public class MyCommand : ICommand
    {

        #region Properties
        //Define an action delegate for Excute method and a predicatone for CanExecute Method
        public Action<object> DelegateForVoid { get; set; }
        public Predicate<object> DelegateForBool { get; set; }

        public event EventHandler CanExecuteChanged;

        #endregion


        #region Constructor
        //Define a Constructor that takes the 2 delegates as argumnets
        public MyCommand(Action<object> _execute, Predicate<object> _CanExecute = null)
        {
            DelegateForVoid = _execute;
            DelegateForBool = _CanExecute;
        }

        #endregion


        #region Methods

        public bool CanExecute(object parameter)
        {
            return DelegateForBool(parameter);
        }

        public void Execute(object parameter)
        {
            DelegateForVoid(parameter);
        }

        #endregion

    }
}
