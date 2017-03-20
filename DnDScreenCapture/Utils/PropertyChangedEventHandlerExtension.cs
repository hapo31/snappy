using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DnDScreenCapture.Utils
{
    public static class PropertyChangedEventHandlerExtension
    {

        /// <summary>
        /// <typeparamref name="PropertyChangedEventHandler"/> の呼び出しを簡略化します。
        /// </summary>
        /// <param name="eh">拡張対象メソッド名</param>
        /// <param name="sender">
        ///     <typeparamref name="PropertyChangedEventHandler"/> の sender と等価です。</param>
        /// <param name="name">
        ///     <typeparamref name="PropertyChangedEventArgs"/> の propertyName と等価です。</param>
        public static void Notice(this PropertyChangedEventHandler eh,
            object sender, [CallerMemberName] string name = "")
        {
            if (eh == null)
            {
                return;
            }
            eh(sender, new PropertyChangedEventArgs(name));
        }
    }
}
