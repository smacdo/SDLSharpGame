using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDL2;
using System.Diagnostics;
using System.IO;

namespace SdlNetTestApp
{
    // TODO: Replace SDL.SDL_Color with custom color class.
    // TODO: Convert the SDL event loop into class with event callbacks that get triggered.
    public class GameException : Exception
    {
        public GameException()
            : base()
        {
        }

        public GameException( string message )
            : base( message )
        {

        }
    }

    public class GameContentLoadException : Exception
    {
        public GameContentLoadException()
            : base( "Failed to load game content" )
        {
        }

        public GameContentLoadException( string filepath )
            : base( "Failed to load game content: " + filepath )
        {

        }
    }


    public class Program
    {
        public static void Main( string[] args )
        {
            InitSDL();
            RunGame();
            CloseSDL();
        }

        private static void InitSDL()
        {
            // Initialize SDL.
            if ( SDL.SDL_Init( SDL.SDL_INIT_VIDEO ) == -1 )
            {
                throw new SDLException();
            }

            // Initialize SDL's font system.
            if ( SDL_ttf.TTF_Init() == -1 )
            {
                throw new SDLException();
            }
        }

        private static void RunGame()
        {
            // Create the main render window.
            SDLWindow window = new SDLWindow( "An SDL2 window", 100, 100, 800, 600 );
            SDLRenderer renderer = new SDLRenderer( window );

            // Load a test image.
            SDLTexture texture = new SDLTexture( renderer, "hello.bmp" );

            SDLFont font = new SDLFont( renderer, "hello.ttf", 24 );
            SDLTexture textTexture = font.RenderTextBlended( renderer, "Hello World!!", Color.BlueColor );

            // Event loop.
            SDL.SDL_Event eventInfo = new SDL.SDL_Event();
            bool shouldQuit = false;

            while ( !shouldQuit )
            {
                CheckForSDLErrors();

                // Pump and process pending OS events.
                while ( SDL.SDL_PollEvent( out eventInfo ) != 0 )
                {
                    if ( eventInfo.type == SDL.SDL_EventType.SDL_QUIT )
                    {
                        shouldQuit = true;
                    }
                }

                // Render all of our stuffs.
                renderer.Clear();
                renderer.Draw( texture, 32, 32 );
                renderer.Draw( textTexture, 0, 400 );
                renderer.Present();    
            }
        }

        private static bool CheckForSDLErrors()
        {
            string error = SDL.SDL_GetError();

            if ( !String.IsNullOrEmpty( error ) )
            {
                Console.WriteLine( "SDL ERROR: {0}", error );
                return true;
            }
            else
            {
                return false;
            }
        }

        private static void CloseSDL()
        {
            // Now exit. Do we actually need to call this? Probably best to be safe...
            SDL.SDL_Quit();
        }
    }
}
