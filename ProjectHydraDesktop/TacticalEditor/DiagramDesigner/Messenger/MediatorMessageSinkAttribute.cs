using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectHydraDesktop.TacticalEditor.DiagramDesigner
{
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class MediatorMessageSinkAttribute : Attribute
    {
        /// <summary>
        /// Message key
        /// </summary>
        public object MessageKey { get; private set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public MediatorMessageSinkAttribute()
        {
            MessageKey = null;
        }

        /// <summary>
        /// Constructor that takes a message key
        /// </summary>
        /// <param name="messageKey">Message Key</param>
        public MediatorMessageSinkAttribute(string messageKey)
        {
            MessageKey = messageKey;
        }
    }
}
