namespace DddShooter
{
    public enum NpcState
    {
        None        = 0,
        Patrol      = 1,
        Pursue      = 2,
        Died        = 3,      // Не нравится это слово здесь, так как приминимо только к
                        // живым по сюжету NPC. Xодячая нежить всегда в этом состоянии. 
                        // Надо выбрать другое слово.
        Inspection  = 4
    }
}
