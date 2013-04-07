using System;
using Microsoft.Xna.Framework;

namespace HoMM.CharacterClasses
{
    /// <summary>
    /// Enum pro zjisteni kam je animace otocena
    /// </summary>
    public enum AnimationKey { Down, Left, Right, Up }

    public class Animation : ICloneable
    {
        #region Fields and Properties

        //Fieldy
        Rectangle[] frames;
        int framesPerSecond;
        TimeSpan frameLength;
        TimeSpan frameTimer;
        int currentFrame;
        int frameWidth;
        int frameHeight;

        //Property
        public int FramesPerSecond
        {
            get { return framesPerSecond; }
            set
            {
                if (value < 1)
                    framesPerSecond = 1;
                else if (value > 60)
                    framesPerSecond = 60;
                else
                    framesPerSecond = value;
                frameLength = TimeSpan.FromSeconds(1 / (double)framesPerSecond);
            }
        }
        public Rectangle CurrentFrameRect
        {
            get { return frames[currentFrame]; }
        }
        public int CurrentFrame
        {
            get { return currentFrame; }
            set
            {
                currentFrame = (int)MathHelper.Clamp(value, 0, frames.Length - 1);
            }
        }
        public int FrameWidth
        {
            get { return frameWidth; }
        }
        public int FrameHeight
        {
            get { return frameHeight; }
        }

        #endregion

        #region Constructors

        private Animation(Animation animation)
        {
            this.frames = animation.frames;
            FramesPerSecond = 5;
        }
        public Animation(int frameCount, int frameWidth, int frameHeight, int xOffset, int yOffset)
        {
            frames = new Rectangle[frameCount];
            this.frameWidth = frameWidth;
            this.frameHeight = frameHeight;

            //Ulozeni rameckuu danych animaci do frames
            for (int i = 0; i < frameCount; i++)
            {
                frames[i] = new Rectangle(
                xOffset + (frameWidth * i),
                yOffset,
                frameWidth,
                frameHeight);
            }
            FramesPerSecond = 5;
            Reset();
        }
        
        #endregion

        #region Methods

        public void Update(GameTime gameTime)
        {
            //Podle herniho casu se meni animace v zavislosti natom, klik obrazku se ma ukazat za sekundu
            frameTimer += gameTime.ElapsedGameTime;
            if (frameTimer >= frameLength)
            {
                frameTimer = TimeSpan.Zero;
                currentFrame = (currentFrame + 1) % frames.Length;
            }
        }

        /// <summary>
        /// Zresetovani animace
        /// </summary>
        public void Reset()
        {
            currentFrame = 0;
            frameTimer = TimeSpan.Zero;
        }

        /// <summary>
        /// Naklonovani jedne animace
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            Animation animationClone = new Animation(this);
            animationClone.frames = this.frames;
            animationClone.frameWidth = this.frameWidth;
            animationClone.frameHeight = this.frameHeight;
            animationClone.Reset();
            return animationClone;
        }

        #endregion
    }
}
