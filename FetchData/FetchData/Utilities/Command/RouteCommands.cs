using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FetchData.Utilities.Command
{
    /// <summary>
    /// 
    /// </summary>
    /// <see cref="System.Windows.Input.ICommand"/>
    public class RouteCommands : ICommand
    {

        #region  private members

        /// <summary>
        /// The action to execute.
        /// </summary>
        private Action myAction = null;

        #endregion


        #region  events

        /// <summary>
        /// Occurs when changes occur that affect whether or not the command should execute.
        /// </summary>
        public event EventHandler CanExecuteChanged = (Sender, e) => { };

        #endregion


        #region  constructor
        /// <summary>
        /// Default constructor
        /// Initializes a new instance of the <see cref="RouteCommands"/> class.
        /// </summary>
        /// <param name="action">The action to execute.</param>
        public RouteCommands(Action action)
        {
            myAction = action;
        }

        #endregion


        #region  methods

        /// <summary>
        /// Defines the methods that determines whether the command can execute in its current state.
        /// </summary>
        /// <param name="parameter">Data used by the command. If the command does not require data to be passed, this object can be set to <see langword="null"/>.</param>
        /// <returns>
        /// <see langword="true"/> if this command can be executed; otherwise, <see langword="false"/>
        /// </returns>
        public bool CanExecute(object parameter)
        {
            return true;
        }

        /// <summary>
        /// Defines the methods to be called when the command is invoked.
        /// </summary>
        /// <param name="parameter">Data used by the command. If the command does not require data to be passed, this object can be set to<see langword="null"/>.</param>
        public void Execute(object parameter)
        {
            myAction();
        }
        #endregion



    }
}
