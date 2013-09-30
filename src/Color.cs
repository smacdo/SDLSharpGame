using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SdlNetTestApp
{
    /// <summary>
    ///  Represents colors.
    ///  TODO: Clamp RGB setters to [0,1].
    /// </summary>
    public struct Color
    {
        private float mR;
        private float mG;
        private float mB;
        private float mA;

        public static readonly Color GreenColor = new Color( 0.0f, 1.0f, 0.0f );
        public static readonly Color BlueColor = new Color( 0.0f, 1.0f, 1.0f );

        public Color( float r, float g, float b )
        {
            mR = r;
            mG = g;
            mB = b;
            mA = 1.0f;
        }

        public float Red
        {
            get { return mR; }
            set { mR = value; }
        }

        public byte RedByte
        {
            get { return (byte)( 255.0f * mR ); }
        }

        public float Green
        {
            get { return mG; }
            set { mR = value; }
        }

        public byte GreenByte
        {
            get { return (byte) ( 255.0f * mG ); }
        }

        public float Blue
        {
            get { return mB; }
            set { mR = value; }
        }

        public byte BlueByte
        {
            get { return (byte) ( 255.0f * mB ); }
        }
    }
}
