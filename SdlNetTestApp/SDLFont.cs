using SDL2;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SdlNetTestApp
{
    /// <summary>
    ///  Encapsulates the SDL font functionality.
    /// </summary>
    public class SDLFont : IDisposable
    {
        private bool mDisposed = false;
        private string mFontName;
        private int mFontSize;
        private IntPtr mFont;

        internal IntPtr FontPtr
        {
            get { return mFont; }
        }

        /// <summary>
        ///  Create a new SDL font.
        /// </summary>
        /// <remarks>
        ///  I'm normally not a huge fan of classes that load themselves, much less a constructor
        ///  that loads itself from a file. I have to make an exception for the moment because
        ///  SDL_image's API only supports loading from a file... once someone (maybe me) adds the
        ///  load from memory calls I can remove this and create a proper byte[] constructor
        ///  instead. (And leave the file loading up to the caller).
        /// </remarks>
        public SDLFont( SDLRenderer renderer, string fontName, int fontSize )
        {
            // Make sure our inputs are valid!
            if ( renderer == null )
            {
                throw new ArgumentNullException( "renderer" );
            }

            if ( String.IsNullOrEmpty( fontName ) )
            {
                throw new ArgumentNullException( "fontName" );
            }

            if ( fontSize <= 0 )
            {
                throw new ArgumentOutOfRangeException( "fontSize" );
            }

            // Make sure the image actually exists on disk before attempting to ask SDL to load it.
            //  (Bonus: SDL doesn't return a null when the image does not exist on disk!)
            if ( !File.Exists( fontName ) )
            {
                throw new FileNotFoundException( fontName );
            }

            // Ask SDL to load our font for us and check if it worked.
            mFont = SDL_ttf.TTF_OpenFont( fontName, fontSize );

            if ( mFont == null )
            {
                throw new SDLException( "Failed to load font" );
            }
        }

        /// <summary>
        ///  Destructor.
        /// </summary>
        ~SDLFont()
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

        public SDLTexture RenderTextBlended( SDLRenderer renderer,
                                             string text,
                                             Color color )
        {
            // Check inputs.
            if ( renderer == null )
            {
                throw new ArgumentNullException( "renderer" );
            }

            // Convert color to SDL format.
            SDL.SDL_Color sdlColor = new SDL.SDL_Color();

            sdlColor.r = color.RedByte;
            sdlColor.g = color.GreenByte;
            sdlColor.b = color.BlueByte;

            // Render the text message to an SDL_Surface and then convert the surface into an
            // SDL texture.
            SDLTexture texture;

            using ( SDLSurface surface = new SDLSurface( SDL_ttf.TTF_RenderText_Blended(
                                                            mFont,
                                                            text,
                                                            sdlColor ) ) )
            {
                texture = new SDLTexture( renderer, surface );
            }

            return texture;
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
                SDL_ttf.TTF_CloseFont( mFont );
                mFont = IntPtr.Zero;

                Console.WriteLine( "Destroyed SDLFont" );

                mDisposed = true;
            }
        }
    }
}
