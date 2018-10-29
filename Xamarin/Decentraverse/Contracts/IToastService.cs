using System;
using System.Collections.Generic;
using System.Text;

namespace Decentraverse.Contracts
{
    /// <summary>
    /// Implemented separately for each platform
    /// </summary>
    public interface IToastService
    {
        void LongAlert(string message);
        void ShortAlert(string message);
    }
}
