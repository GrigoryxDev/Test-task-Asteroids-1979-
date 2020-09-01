using Scripts.Enemies;
using UnityEngine;

namespace Scripts.ScreenWrap
{
    public class WrapScreen : TransformOfScreen
    {
        private BaseEnemy enemy;
        private void Start()
        {
            enemy = GetComponent<BaseEnemy>();
        }
        public override void Update()
        {
            base.Update();

            // if IsOffscreen, convert viewport pos back to world pos and apply to transform.
            if (isOffscreen)
            {
                transform.position = Camera.main.ViewportToWorldPoint(viewportPos);
                if (enemy != null)
                {
                    enemy.StartMoving();
                }
            }
        }
    }
}