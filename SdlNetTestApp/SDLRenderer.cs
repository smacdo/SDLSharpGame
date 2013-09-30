using SDL2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SdlNetTestApp
{
    /// <summary>
    ///  Encapsulates the SDL renderer functionality.
    /// </summary>
    public class SDLRenderer : IDisposable
    {
        private bool mDisposed = false;
        private IntPtr mRenderer;

        internal IntPtr RendererPtr
        {
            get { return mRenderer; }
        }

        /// <summary>
        ///  Create a new SDL renderer.
        /// </summary>
        /// <param name="surface">Pointer to unmanaged SDL_Surface.</param>
        public SDLRenderer( SDLWindow window )
        {
            // Make sure the window is valid!
            if ( window == null )
            {
                throw new ArgumentNullException( "window" );
            }

            // Generate appropriate renderer flags.
            uint renderFlags = (uint) SDL.SDL_RendererFlags.SDL_RENDERER_ACCELERATED |
                               (uint) SDL.SDL_RendererFlags.SDL_RENDERER_PRESENTVSYNC;

            // Create the renderer object and ensure that it was actually instantiated.
            mRenderer = SDL.SDL_CreateRenderer( window.WindowPtr, -1, renderFlags );

            if ( mRenderer == IntPtr.Zero )
            {
                throw new SDLException( "Failed to create SDL renderer" );
            }
        }

        /// <summary>
        ///  Destructor.
        /// </summary>
        ~SDLRenderer()
        {
            Dispose( false );
        }

        /// <summary>
        ///  Dispose the SDL renderer.
        /// </summary>
        public void Dispose()
        {
            Dispose( true );
            GC.SuppressFinalize( this );
        }

        /// <summary>
        ///  Destroys the unmanaged SDL renderer.
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
                SDL.SDL_DestroyRenderer( mRenderer );
                mRenderer = IntPtr.Zero;
                
                Console.WriteLine( "Destroyed SDLRenderer" );

                mDisposed = true;
            }
        }

        /// <summary>
        ///  Clear the back buffer.
        /// </summary>
        public void Clear()
        {
            SDL.SDL_RenderClear( mRenderer );
        }

        public void Draw( SDLTexture texture, int x, int y )
        {
            // Sanity check our inputs, make sure they are valid.
            if ( texture == null )
            {
                throw new ArgumentNullException( "texture" );
            }

            if ( x < 0 )
            {
                throw new ArgumentOutOfRangeException( "x" );
            }
            else if ( y < 0 )
            {
                throw new ArgumentOutOfRangeException( "y" );
            }

            // Set up the target rectangle, which is the area on the screen that this texture will
            // be drawn to.
            SDL.SDL_Rect target = new SDL.SDL_Rect();

            target.x = x;
            target.y = y;
            target.w = texture.Width;
            target.h = texture.Height;

            // Set up the source rectangle, which is the area of the texture that will be drawn to
            // the screen.
            SDL.SDL_Rect source = new SDL.SDL_Rect();

            source.x = 0;
            source.y = 0;
            source.w = texture.Width;
            source.h = texture.Height;

            // Now issue the draw command to SDL.
            SDL.SDL_RenderCopy( mRenderer, texture.TexturePtr, ref source, ref target );
        }

        /// <summary>
        ///  Present the back buffer to the display.
        /// </summary>
        public void Present()
        {
            SDL.SDL_RenderPresent( mRenderer );
        }
    }
     
}
