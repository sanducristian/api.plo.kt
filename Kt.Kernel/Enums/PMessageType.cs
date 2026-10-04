using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kt.Kernel;



/// <summary>
/// The type of messages that are
/// </summary>
public enum PMessageType {
    Undefined = 0,          // An undefined message, do nothing
    Trace = 10,             // A trace information for application tracking (debug mode)
    Status = 20,            // A message that can be displayed to the user only to see the current status
    Success = 30,           // Show that the last event ended with success
    Information = 40,       // A message that can be shown to the user that can be ignored
    Question = 50,          // A message where the user should enter its choice
    Help = 60,              // A message that shows the user what the application is expecting from him
    Warning = 70,           // A message that should be shown to the user in order to inform him about eventual problems  
    Denied = 80,            // A message showing that the user cannot access that particular function
    Shield = 90,            // A message showing that the user can access that particular function with an additional required authentification
    Error = 100,            // A message that must be displayed to the user in order to take proper action
    Exception = 110         // A message that must be displayed to the user when a system is malfunctioning
}

/*
 * 
 * 
 *
 public enum PMessageType : uint  {
    None = 0,
    Information = 1,
    Question = 2,
    Success = 3,
    Warning = 4,
    Error = 5,
    Denied = 6,
    Help = 7,
    Shield = 8
}
*/

