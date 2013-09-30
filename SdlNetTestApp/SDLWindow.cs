using SDL2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SdlNetTestApp
{
    /// <summary>
    ///  Controls an unmanaged SDL window.
    /// </summary>
    public class SDLWindow : IDisposable
    {
        private bool mDisposed = false;
        private IntPtr mWindow;

        internal IntPtr WindowPtr
        {
            get { return mWindow; }
        }

        /// <summary>
        ///  Create a new SDL surface.
        /// </summary>
        /// <param name="surface">Pointer to unmanaged SDL_Surface.</param>
        internal SDLWindow( string windowTitle,
                            int x,
                            int y,
                            int width,
                            int height )
        {
            SDL.SDL_WindowFlags flags = SDL.SDL_WindowFlags.SDL_WINDOW_SHOWN;
            mWindow = SDL.SDL_CreateWindow( windowTitle, x, y, width, height, flags );

            if ( mWindow == IntPtr.Zero )
            {
                throw new SDLException( "Failed to create main window" );
            }
        }

        /// <summary>
        ///  Destructor.
        /// </summary>
        ~SDLWindow()
        {
            Dispose( false );
        }

        /// <summary>
        ///  Dispose the SDL window.
        /// </summary>
        public void Dispose()
        {
            Dispose( true );
            GC.SuppressFinalize( this );
        }

        /// <summary>
        ///  Destroys the unmanaged SDL window.
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose( bool disposing )
        {
            if ( !mDisposed )
            {
                if ( disposing )
                {
                    // Free any managed objects.
                }

                // Free your own state (unmanaged objects).
                SDL.SDL_DestroyWindow( mWindow );
                mWindow = IntPtr.Zero;

                Console.WriteLine( "Destroyed SDLWindow" );

                mDisposed = true;
            }
        }
    }
}
