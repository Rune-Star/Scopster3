using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using TMPro;

public static class DOTweenTMP
{
    /// <summary>Tweens a TMP's text to the given value.
    /// Also stores the Text as the tween's target so it can be used for filtered operations</summary>
    /// <param name="endValue">The end string to tween to</param><param name="duration">The duration of the tween</param>
    /// <param name="richTextEnabled">If TRUE (default), rich text will be interpreted correctly while animated,
    /// otherwise all tags will be considered as normal text</param>
    /// <param name="scrambleMode">The type of scramble mode to use, if any</param>
    /// <param name="scrambleChars">A string containing the characters to use for scrambling.
    /// Use as many characters as possible (minimum 10) because DOTween uses a fast scramble mode which gives better results with more characters.
    /// Leave it to NULL (default) to use default ones</param>
    public static TweenerCore<string, string, StringOptions> DO_TMP_TEXT(this TMP_Text target, string endValue,
        float duration, bool richTextEnabled = true,
        ScrambleMode scrambleMode = ScrambleMode.None, string scrambleChars = null)
    {
        var t = DOTween.To(() => target.text, x => target.text = x, endValue,
            duration);
        t.SetOptions(richTextEnabled, scrambleMode, scrambleChars)
            .SetTarget(target);
        return t;
    }

    /// <summary>Tweens a TMP's text to the given value.
    /// Also stores the Text as the tween's target so it can be used for filtered operations</summary>
    /// <param name="duration">The duration of the tween</param>
    /// <param name="richTextEnabled">If TRUE (default), rich text will be interpreted correctly while animated,
    /// otherwise all tags will be considered as normal text</param>
    /// <param name="scrambleMode">The type of scramble mode to use, if any</param>
    /// <param name="scrambleChars">A string containing the characters to use for scrambling.
    /// Use as many characters as possible (minimum 10) because DOTween uses a fast scramble mode which gives better results with more characters.
    /// Leave it to NULL (default) to use default ones</param>
    public static TweenerCore<string, string, StringOptions> DO_TMP_TEXT(this TMP_Text target,
        float duration, bool richTextEnabled = true,
        ScrambleMode scrambleMode = ScrambleMode.None, string scrambleChars = null)
    {
        return DO_TMP_TEXT(target, target.text[^1].ToString(), duration, richTextEnabled, scrambleMode,
            scrambleChars);
    }

    /// <param name="duration">The duration of the tween</param>
    /// <param name="richTextEnabled">If TRUE (default), rich text will be interpreted correctly while animated,
    /// otherwise all tags will be considered as normal text</param>
    /// <param name="scrambleMode">The type of scramble mode to use, if any</param>
    /// <param name="scrambleChars">A string containing the characters to use for scrambling.
    /// Use as many characters as possible (minimum 10) because DOTween uses a fast scramble mode which gives better results with more characters.
    /// Leave it to NULL (default) to use default ones</param>
    public static Tweener DO_TMP(this TMP_Text text, float duration, bool richTextEnabled = true,
        ScrambleMode scrambleMode = ScrambleMode.None, string scrambleChars = null)
    {
        text.maxVisibleCharacters = 0;
        var t = DOTween.To(x => text.maxVisibleCharacters = (int) x, 0, text.text.Length, duration);
        return t;
    }
}