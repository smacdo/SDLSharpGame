using SDL2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SdlNetTestApp
{
    // TODO: Where on earth is TTF_Error?
    // TODO: Create font system. Basically go ahead and pre-render each character into a surface and
    //       then draw this rather than make new surfaces each time. Or maybe have a sprite font system?
    public class SDLException : Exception
    {
        public SDLException()
            : base( SDL.SDL_GetError() )
        {
        }

        public SDLException( string message )
            : base( message + ". SDL: " + SDL.SDL_GetError() )
        {

        }
    }

}
