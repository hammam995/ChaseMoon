using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    bool canMove { get; }
    float coefficientSpeed { get; }
    void Use();
}
public interface IKillable
{
    void Kill();
}