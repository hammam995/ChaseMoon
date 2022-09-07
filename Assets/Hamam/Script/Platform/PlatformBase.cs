using UnityEngine;

public abstract class PlatformBase  
{
    // Platform Base State abstract , all platforms scripts will have these methoods
        public abstract void EnterState(PlatformManager MainPlatform);
        public abstract void UpdateState(PlatformManager MainPlatform);
        public abstract void OnTriggerEnter(PlatformManager MainPlatform , Collider2D other);
        public abstract void OnTriggerExit(PlatformManager MainPlatform , Collider2D other);
    // i have to update the enter and exit from the monobegavior make them connected
    
}
