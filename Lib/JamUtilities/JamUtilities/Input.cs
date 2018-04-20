using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JamUtilities
{
    public class Input
    {

        /// <summary>
        /// is the key down in this frame
        /// </summary>
        public static Dictionary<SFML.Window.Keyboard.Key, bool> pressed { get; private set; } = null;
        
        /// <summary>
        /// is the key up in this frame
        /// </summary>
        public static Dictionary<SFML.Window.Keyboard.Key, bool> released { get; private set; } = null;
        
        /// <summary>
        /// has the key been pressed down in this frame (but not in the last)
        /// </summary>
        public static Dictionary<SFML.Window.Keyboard.Key, bool> justPressed { get; private set; }  = null;

        /// <summary>
        /// has the key been released in this frame (but was pressed in the last)
        /// </summary>
        public static Dictionary<SFML.Window.Keyboard.Key, bool> justReleased { get; private set; } = null;

        private static List<Keyboard.Key> allKeys = null;

        private static void Init()
        {
            pressed = new Dictionary<Keyboard.Key, bool>();
            setupDict(pressed);
            released = new Dictionary<SFML.Window.Keyboard.Key, bool>();
            setupDict(released);
            justPressed = new Dictionary<SFML.Window.Keyboard.Key, bool>();
            setupDict(justPressed);
            justReleased = new Dictionary<SFML.Window.Keyboard.Key, bool>();
            setupDict(justReleased);
        }

        private static void setupDict(Dictionary<Keyboard.Key, bool> dict)
        {
            
                var _allKeys = EnumUtil.GetValues<Keyboard.Key>();

            foreach (var k in _allKeys)
                dict.Add(k, false);

            if (allKeys == null)
                allKeys = new List<Keyboard.Key>(dict.Keys);
        }
        public static void Reset (Dictionary<Keyboard.Key,bool> dict)
        {
            foreach(var kvp in dict)
                dict[kvp.Key] = false;
        }

        

        private static bool hasBeenInitialized()
        {
            return pressed != null;
        }

        public static void Update()
        {
            if (!hasBeenInitialized())
                Init();

            



         
            foreach(var currentKey in allKeys)
            {
                if (Keyboard.IsKeyPressed(currentKey))
                {
                    if (pressed[currentKey] == false)
                        justPressed[currentKey] = true;
                    else
                        justPressed[currentKey] = false;
                    
                    pressed[currentKey] = true;
                    released[currentKey] = false;
                }
                else
                {
                    if (pressed[currentKey] == true)
                        justReleased[currentKey] = true;
                    else
                        justReleased[currentKey] = false;

                    pressed[currentKey] = false;
                    released[currentKey] = true;
                }
            }    

        }

    }
}
