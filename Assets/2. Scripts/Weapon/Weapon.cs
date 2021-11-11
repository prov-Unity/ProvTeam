using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public enum AttackCheckMode
{
    Enable,
    Overlap
}

[RequireComponent(typeof(Collider))]
public class Weapon : MonoBehaviour
{
    public AttackCheckMode checkMode = AttackCheckMode.Enable;
    // 콜라이더 활성 비활성화로 공격판정할 떄 쓰임
    [ReadOnly] public Collider _col;
    [ReadOnly] public Player owner;
    public int attackPower;

    // OverlapBox로 공격판정할 때 쓰임
    public Vector3 center;

    private void OnDrawGizmos()
    {
        if (_col == null)
            _col = GetComponent<BoxCollider>();

        Gizmos.color = new Color(0.3f, 0.22f, 0.6f, 0.6f);
        Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, transform.lossyScale);

        if (_col.GetType() == typeof(BoxCollider))
        {
            var collider = (BoxCollider) _col;
            Gizmos.DrawCube(collider.center, Vector3.Scale(Vector3.one, collider.size));
        }
        else if (_col.GetType() == typeof(CapsuleCollider))
        {
            var collider = (CapsuleCollider) _col;
            DrawWireCapsule(collider.center,
                            Quaternion.identity,
                            collider.radius,
                            0f,
                            new Color(0.3f,
                                0.22f,
                                0.6f,
                                0.6f));
        }
        if (_col.GetType() == typeof(SphereCollider))
        {
            var collider = (SphereCollider) _col;
            Gizmos.DrawSphere(collider.center, collider.radius);
        }
    }
    
    public void DrawWireCapsule(Vector3 _pos, Quaternion _rot, float _radius, float _height, Color _color = default(Color))
    {
        if (_color != default(Color))
            Handles.color = _color;
        Matrix4x4 angleMatrix = Matrix4x4.TRS(_pos, _rot, Handles.matrix.lossyScale);
        using (new Handles.DrawingScope(angleMatrix))
        {
            var pointOffset = (_height - (_radius * 2)) / 2;
 
            //draw sideways
            Handles.DrawWireArc(Vector3.up * pointOffset, Vector3.left, Vector3.back, -180, _radius);
            Handles.DrawLine(new Vector3(0, pointOffset, -_radius), new Vector3(0, -pointOffset, -_radius));
            Handles.DrawLine(new Vector3(0, pointOffset, _radius), new Vector3(0, -pointOffset, _radius));
            Handles.DrawWireArc(Vector3.down * pointOffset, Vector3.left, Vector3.back, 180, _radius);
            //draw frontways
            Handles.DrawWireArc(Vector3.up * pointOffset, Vector3.back, Vector3.left, 180, _radius);
            Handles.DrawLine(new Vector3(-_radius, pointOffset, 0), new Vector3(-_radius, -pointOffset, 0));
            Handles.DrawLine(new Vector3(_radius, pointOffset, 0), new Vector3(_radius, -pointOffset, 0));
            Handles.DrawWireArc(Vector3.down * pointOffset, Vector3.back, Vector3.left, -180, _radius);
            //draw center
            Handles.DrawWireDisc(Vector3.up * pointOffset, Vector3.up, _radius);
            Handles.DrawWireDisc(Vector3.down * pointOffset, Vector3.up, _radius);
 
        }
    }

    private void Awake()
    {
        _col = GetComponent<BoxCollider>();
    }

    public void CheckAttackTarget()
    {

    }

    /// <summary>
    /// 특정 프레임 구간동안 트리거를 활성화 시켜주기 위한 방법
    /// </summary>
    /// <param name="isEnable"></param>
    public void EnableCollider(bool isEnable)
    {
        _col.enabled = isEnable;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerCombat>().GetDamaged(attackPower);
        }
    }
}