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
    [ReadOnly] public WeaponType weaponType;
    [ReadOnly] public int attackPower;
    [ReadOnly] public int durability;

    [ReadOnly] public string owner;

    // OverlapBox로 공격판정할 때 쓰임
    public Vector3 center;

    private void OnDrawGizmos()
    {
        if (_col == null)
            _col = GetComponent<Collider>();

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
                0,
                collider.radius);
        }
        if (_col.GetType() == typeof(SphereCollider))
        {
            var collider = (SphereCollider) _col;
            Gizmos.DrawSphere(collider.center, collider.radius);
        }
    }
    
     private void DrawSolidCapsule(Vector3 center, float height, float radius)
    {
        
        var upper = center + Vector3.up * (height - 1) * 0.5f;
        var lower = center - Vector3.up * (height - 1) * 0.5f;
        var offsetX = new Vector3(radius, 0f, 0f);
        var offsetZ = new Vector3(0f, 0f, radius);
        
        //draw frontways
        Handles.DrawSolidArc(upper, Vector3.back, Vector3.left, 180, radius);
        Handles.DrawLine(lower + offsetX, upper + offsetX);
        Handles.DrawLine(lower - offsetX, upper - offsetX);
        Handles.DrawSolidArc(lower, Vector3.back, Vector3.left, -180, radius);
        
        //draw sideways
        Handles.DrawSolidArc(upper, Vector3.left, Vector3.back, -180, radius);
        Handles.DrawLine(lower + offsetZ, upper + offsetZ);
        Handles.DrawLine(lower - offsetZ, upper - offsetZ);
        Handles.DrawSolidArc(lower, Vector3.left, Vector3.back, 180, radius);
        
        //draw center
        Handles.DrawSolidDisc(upper, Vector3.up, radius);
        Handles.DrawSolidDisc(lower, Vector3.up, radius);
    }
    
    private void DrawWireCapsule(Vector3 center, float height, float radius)
    {
        
        var upper = center + Vector3.up * (height - 1) * 0.5f;
        var lower = center - Vector3.up * (height - 1) * 0.5f;
        var offsetX = new Vector3(radius, 0f, 0f);
        var offsetZ = new Vector3(0f, 0f, radius);
        
        //draw frontways
        Handles.DrawWireArc(upper, Vector3.back, Vector3.left, 180, radius);
        Handles.DrawLine(lower + offsetX, upper + offsetX);
        Handles.DrawLine(lower - offsetX, upper - offsetX);
        Handles.DrawWireArc(lower, Vector3.back, Vector3.left, -180, radius);
        
        //draw sideways
        Handles.DrawWireArc(upper, Vector3.left, Vector3.back, -180, radius);
        Handles.DrawLine(lower + offsetZ, upper + offsetZ);
        Handles.DrawLine(lower - offsetZ, upper - offsetZ);
        Handles.DrawWireArc(lower, Vector3.left, Vector3.back, 180, radius);
        
        //draw center
        Handles.DrawWireDisc(upper, Vector3.up, radius);
        Handles.DrawWireDisc(lower, Vector3.up, radius);
    }

    private void Awake()
    {
        SetOwner();
        _col = GetComponent<Collider>();
    }

    public void SetOwner()
    {
        string tagName = transform.parent.tag;
        if (tagName.Equals("Player"))
            owner = "Player";
        else if (tagName.Equals("Monster"))
            owner = "Monster";
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