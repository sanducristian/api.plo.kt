using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QS.Kernel.Lists {
	public interface PIRemoteList {

		void Add(object obj);
		bool Remove(object obj);

		/// <summary>
		/// Add the object specified by the parameter
		/// </summary>
		/// <param name="obj">The object to be added to the list</param>
		//void RemoteAddItem(object obj);


		/// <summary>
		/// Remove the item specified by the object
		/// </summary>
		/// <param name="obj">Object to be removed</param>
		//void RemoteRemoveItem(object obj);
	}
}
