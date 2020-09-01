using UnityEngine;

namespace Scripts.ScreenWrap
{
    public class WrapScreen : TransformOfScreen
    {
        public override void Update()
        {
            base.Update();

            // if IsOffscreen, convert viewport pos back to world pos and apply to transform.
            if (isOffscreen)
            {
                transform.position = Camera.main.ViewportToWorldPoint(viewportPos);
            }
        }
    }
}