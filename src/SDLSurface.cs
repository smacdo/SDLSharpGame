using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDL2;

namespace SdlNetTestApp
{
    /// <summary>
    ///  Controls an unmanaged SDL surface.
    /// </summary>
    public class SDLSurface : IDisposable
    {
        private bool mDisposed = false;
        private IntPtr mSurface;

        internal IntPtr SurfacePtr
        {
            get { return mSurface; }
        }

        /// <summary>
        ///  Create a new SDL surface.
        /// </summary>
        /// <param name="surface">Pointer to unmanaged SDL_Surface.</param>
        internal SDLSurface( IntPtr surface )
        {
            if ( surface == IntPtr.Zero )
            {
                throw new ArgumentNullException( "surface" );
            }

            mSurface = surface;
        }

        /// <summary>
        ///  Destructor.
        /// </summary>
        ~SDLSurface()
        {
            Dispose( false );
        }

        /// <summary>
        ///  Dispose the SDL surface.
        /// </summary>
        public void Dispose()
        {
            Dispose( true );
            GC.SuppressFinalize( this );
        }

        /// <summary>
        ///  Destroys the unmanaged SDL surface.
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
                SDL.SDL_FreeSurface( mSurface );
                mSurface = IntPtr.Zero;

                Console.WriteLine( "Destroyed SDLSurface" );
                
                mDisposed = true;
            }
        }
    }
}
