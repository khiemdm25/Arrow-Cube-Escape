public static class InputState
{
    public static bool IsZooming = false;
    public static bool IsRotating = false;
    public static bool IsInteracting => IsZooming || IsRotating;
}