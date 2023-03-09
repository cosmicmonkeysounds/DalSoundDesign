using UnityEngine;
using System;
using System.Runtime.CompilerServices;

public static class Extensions
{
    const MethodImplOptions INLINE = MethodImplOptions.AggressiveInlining;

    [MethodImpl( INLINE )] public static float Lerp( float a, float b, float t ) => ( 1f - t ) * a + t * b;
    [MethodImpl( INLINE )] public static float InverseLerp( float a, float b, float value ) => ( value - a ) / ( b - a );
    [MethodImpl( INLINE )] public static float Remap(this float value, float iMin, float iMax, float oMin, float oMax) => Lerp( oMin, oMax, InverseLerp( iMin, iMax, value ) );
}