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
    ///  Controls an unmanaged SDL texture.
    /// </summary>
    public class SDLTexture : IDisposable
    {
        private bool mDisposed = false;
        private IntPtr mTexture;
        private int mWidth;
        private int mHeight;

        /// <summary>
        ///  Create a new SDL texture.
        /// </summary>
        /// <param name="surface"></param>
        public SDLTexture( SDLRenderer renderer, SDLSurface surface )
        {
            if ( renderer == null )
            {
                throw new ArgumentNullException( "renderer" );
            }

            if ( surface == null )
            {
                throw new ArgumentNullException( "surface" );
            }

            // Convert the SDL surface into an SDL texture.
            mTexture = SDL.SDL_CreateTextureFromSurface( renderer.RendererPtr,
                                                         surface.SurfacePtr );

            if ( mTexture == null )
            {
                throw new SDLException( "Failed to convert SDL surface to texture" );
            }

            ReadAndStoreTextureProperties();
        }

        /// <summary>
        ///  Create a new SDL texture from a file on disk.
        /// </summary>
        /// <remarks>
        ///  I'm normally not a huge fan of classes that load themselves, much less a constructor
        ///  that loads itself from a file. I have to make an exception for the moment because
        ///  SDL_image's API only supports loading from a file... once someone (maybe me) adds the
        ///  load from memory calls I can remove this and create a proper byte[] constructor
        ///  instead. (And leave the file loading up to the caller).
        /// </remarks>
        /// <param name="surface"></param>
        public SDLTexture( SDLRenderer renderer, string imagePath )
        {
            // Verify that our references are valid before proceeding.
            if ( renderer == null )
            {
                throw new ArgumentNullException( "renderer" );
            }

            if ( imagePath == null )
            {
                throw new ArgumentNullException( "imagePath" );
            }

            // Make sure the image actually exists on disk before attempting to ask SDL to load it.
            //  (Bonus: SDL doesn't return a null when the image does not exist on disk!)
            if ( !File.Exists( imagePath ) )
            {
                throw new FileNotFoundException( imagePath );
            }

            // Attempt to load the bitmap from disk.
            mTexture = SDL_image.IMG_LoadTexture( renderer.RendererPtr, imagePath );

            // Test to make sure the texture was successfully created.
            if ( mTexture == null )
            {
                throw new SDLException( "Failed to load image from disk into SDL texture" );
            }

            ReadAndStoreTextureProperties();
        }

        /// <summary>
        ///  Create a new SDL texture.
        /// </summary>
        /// <param name="surface">Pointer to unmanaged SDL_Surface.</param>
        internal SDLTexture( IntPtr texture )
        {
            if ( texture == IntPtr.Zero )
            {
                throw new ArgumentNullException( "texture" );
            }

            mTexture = texture;
            ReadAndStoreTextureProperties();
        }

        /// <summary>
        ///  Destructor.
        /// </summary>
        ~SDLTexture()
        {
            Dispose( false );
        }

        /// <summary>
        ///  Internal SDL texture pointer.
        /// </summary>
        internal IntPtr TexturePtr
        {
            get { return mTexture; }
        }

        /// <summary>
        ///  Get the height of the texture.
        /// </summary>
        public int Height
        {
            get { return mHeight; }
        }

        /// <summary>
        ///  Get the width of the texture.
        /// </summary>
        public int Width
        {
            get { return mWidth; }
        }

        /// <summary>
        ///  Dispose the SDL texture.
        /// </summary>
        public void Dispose()
        {
            Dispose( true );
            GC.SuppressFinalize( this );
        }

        /// <summary>
        ///  Destroys the unmanaged SDL texture.
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
                SDL.SDL_DestroyTexture( mTexture );
                mTexture = IntPtr.Zero;

                Console.WriteLine( "Destroyed SDLTexture" );

                mDisposed = true;
            }
        }

        private void ReadAndStoreTextureProperties()
        {

            // Query for texture dimensions and format.
            //  TODO: SDL_QueryTexture C# stub is busted.... the pixelFormat is supposed to be a
            //        pointer. IS this going to leak??
            int pointerThrowAway;
            uint pixelFormatPointer;

            SDL.SDL_QueryTexture( mTexture,
                                  out pixelFormatPointer,
                                  out pointerThrowAway,
                                  out mWidth,
                                  out mHeight );
        }
     
    }
}
