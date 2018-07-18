using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project
{
    enum PlayerState
    {
        IDEL = 0,
        DEAD ,
    }

    enum AnimState
    {
        ANIM_MOVE,
    }

    public class PlayerController : Controller
    {
        //玩家移速
        float speed;
        //玩家状态
        int state;
        //动画
        [HideInInspector]
        public Animator animator;
        //图片渲染
        [HideInInspector]
        public SpriteRenderer spriteRender;
        //刚体
        [HideInInspector]
        public Rigidbody2D rigidbody;
        //朝向精灵
        [HideInInspector]
        public Sprite[] directionsSprites;

        void Awake()
        {
            speed = 20;
            state = 0;
            animator = gameObject.GetComponentInChildren<Animator>();
            spriteRender = gameObject.GetComponentInChildren<SpriteRenderer>();
            rigidbody = gameObject.GetComponent<Rigidbody2D>();
            directionsSprites = Resources.LoadAll<Sprite>("Texture2D/player_direction");
            //KeyCmdManager = SettingManager.GetInstance();
        }

        void FixedUpdate()
        {
            SettingManager.GetInstance().UpdatePlayerCmd();
            //setPlayerForward();
        }

        //控制动画的朝向
        private void setPlayerForward()
        {
            Vector2 centerPos = new Vector2(0, 0);
            centerPos.x = Screen.width / 2;
            centerPos.y = Screen.height / 2;
            Vector2 mousePos = Input.mousePosition;
            Vector2 fromVector = mousePos - centerPos;
            Vector2 toVector = new Vector2(centerPos.x, mousePos.y - centerPos.y);

            float angle = Vector2.Angle(fromVector, toVector); //求出两向量之间的夹角
            if (mousePos.y < centerPos.y)
            {
                angle = - angle;
            }
            //Debug.Log("夹角: " + angle);
            Quaternion _rotation = Quaternion.identity;
            _rotation.eulerAngles = new Vector3(0, 0, angle);
            
             //.rotation = _rotation; 

        }

        public override void onCmd_Controller_Move(Vector2 movedir)
        {
            int dir = (int)(movedir.x * 3 + movedir.y);
            if (dir == 2) dir = 3;
            if (dir == -4) dir = -3;
            animator.SetInteger("moveDir", dir);
            animator.SetBool("isClickMoveBtn", true);
            //-2     1    4
            //-3          3
            //      -1     
            //松开方向按键
            if (dir == 0)
            {
                animator.SetBool("isClickMoveBtn", false);
                return;
            }

            Vector2 targetPos = new Vector2(
              this.transform.position.x, this.transform.position.y
              ) + movedir;

            rigidbody.MovePosition(
                Vector2.Lerp(rigidbody.position, targetPos, speed * Time.deltaTime));
        }

    }
}
