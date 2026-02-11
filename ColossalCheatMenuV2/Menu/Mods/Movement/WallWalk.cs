using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.XR;
using static Colossal.Plugin;

namespace Colossal.Mods
{
    public class WallWalk : MonoBehaviour
    {
        private Vector3 normal2;
        private Vector3 vel1;
        private Vector3 vel2;
        private float dist2;
        private int layers;
        private bool LeftClose2;
        private bool DoOnce2;
        private float maxD2;

        private float ammount;
        public void Update()
        {
            if(Menu.Menu.Movement[2].stringsliderind != null)
            {
                switch (Menu.Menu.Movement[2].stringsliderind)
                {
                    case 0:
                        ammount = 0;
                        break;
                    case 1:
                        ammount = 7.5f;
                        break;
                    case 2:
                        ammount = 7.9f;
                        break;
                    case 3:
                        ammount = 8.3f;
                        break;
                    case 4:
                        ammount = 8.7f;
                        break;
                    case 5:
                        ammount = 9.1f;
                        break;
                    case 6:
                        ammount = 9.5f;
                        break;
                }
            }
            if (ControllerInputPoller.instance.rightGrab && ammount > 0)
            {
                if (!this.DoOnce2)
                {
                    this.maxD2 = 1f;
                    this.layers = int.MaxValue;
                    this.DoOnce2 = true;
                }
                RaycastHit raycastHit;
                Physics.Raycast(GorillaTagger.Instance.rightHandTransform.position, -GorillaTagger.Instance.rightHandTransform.right, out raycastHit, 1f, this.layers);
                RaycastHit raycastHit2;
                Physics.Raycast(GorillaTagger.Instance.leftHandTransform.position, GorillaTagger.Instance.leftHandTransform.right, out raycastHit2, 1f, this.layers);
                if (raycastHit2.distance > raycastHit.distance)
                {
                    this.normal2 = raycastHit.normal;
                    this.dist2 = raycastHit.distance;
                }
                else
                {
                    this.normal2 = raycastHit2.normal;
                    this.dist2 = raycastHit2.distance;
                    this.LeftClose2 = true;
                }
                if (this.dist2 < this.maxD2)
                {
                    this.vel2 = this.normal2 * (ammount * Time.deltaTime);
                    GorillaTagger.Instance.bodyCollider.attachedRigidbody.linearVelocity -= this.vel2;
                }
                else
                {
                    GorillaTagger.Instance.bodyCollider.attachedRigidbody.useGravity = true;
                }
            }
            else
            {
                GorillaTagger.Instance.bodyCollider.attachedRigidbody.useGravity = true;
            }
        }
    }
}
