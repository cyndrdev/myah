public static class Generics
{
    public static float EaseInOutQuad(float start, float end, float value)
    {
        value /= 0.5f;
        end -= start;
        if (value < 1) return end * 0.5f * value * value + start;
        value--;
        return -end * 0.5f * (value * (value - 2) - 1) + start;
    }

    public static float EaseInOutQuad(float value)
    {
        return EaseInOutQuad(0.0f, 1.0f, value);
    }
}
