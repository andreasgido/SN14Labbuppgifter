using System;
using System.IO;

namespace Laboration4.A.Utils
{
    /// <summary>
    /// Fake null-stream för att hantera Console-fönstret vid tester
    /// </summary>
    public class NullOut : IDisposable
    {
        private bool _disposed = false;
        private TextWriter _newOut;
        private TextWriter _oldOut;
        private MemoryStream _stream;

        public NullOut()
        {
            this._oldOut = Console.Out;
            this._stream = new MemoryStream(0);
            this._newOut = new StreamWriter(this._stream);
            Console.SetOut(this._newOut);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    Console.SetOut(this._oldOut);
                    this._newOut.Dispose();
                    this._stream.Dispose();
                }
                this._disposed = true;
            }
        }

        ~NullOut()
        {
            Dispose(false);
        }
    }
}