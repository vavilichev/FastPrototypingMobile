using UnityEngine;

public struct RectBounds {
    public Vector2 bottomLeftPoint { get; private set; }
    public Vector2 topRightPoint { get; private set; }
    public Vector2 size { get; }

    public RectBounds(Vector3[] worldCorners, Vector2 size, Camera camera, RectTransform rootRectTransform) {
        float xMin = 99999999f;
        float yMin = 99999999f;

        int count = worldCorners.Length;
        for (int i = 0; i < count; i++) {
            Vector3 point = worldCorners[i];

            if (point.x < xMin)
                xMin = point.x;

            if (point.y < yMin)
                yMin = point.y;
        }
        
        Vector3 screenPoint = camera.WorldToScreenPoint(new Vector2(xMin, yMin));
        Vector2 result;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rootRectTransform, screenPoint, camera, out result);
        this.bottomLeftPoint = result;
                
        //this.bottomLeftPoint = camera.WorldToScreenPoint(new Vector2(xMin, yMin));
        this.topRightPoint = new Vector2(this.bottomLeftPoint.x + size.x, this.bottomLeftPoint.y + size.y);
        this.size = size;
    }

    public bool ContainsFull(RectBounds otherRectBounds) {
        
        Vector2 otherBottomLeftPoint = otherRectBounds.bottomLeftPoint;
        if (otherBottomLeftPoint.x < this.bottomLeftPoint.x
            || otherBottomLeftPoint.x > this.topRightPoint.x
            || otherBottomLeftPoint.y < this.bottomLeftPoint.y
            || otherBottomLeftPoint.y > this.topRightPoint.y)
            return false;

        Vector2 otherTopRightPoint = otherRectBounds.topRightPoint;
        if (otherTopRightPoint.x < this.bottomLeftPoint.x
            || otherTopRightPoint.x > this.topRightPoint.x
            || otherTopRightPoint.y < this.bottomLeftPoint.y
            || otherTopRightPoint.y > this.topRightPoint.y)
            return false;

        return true;
    }

    public bool ContainsAny(RectBounds otherRectBounds) {

        Vector2 cachedPoint = otherRectBounds.bottomLeftPoint;
        if (this.ContainsPoint(cachedPoint))
            return true;
        
        cachedPoint = new Vector2(otherRectBounds.bottomLeftPoint.x, otherRectBounds.topRightPoint.y);
        if (this.ContainsPoint(cachedPoint))
            return true;

        cachedPoint = otherRectBounds.topRightPoint;
        if (this.ContainsPoint(cachedPoint))
            return true;


        cachedPoint = new Vector2(otherRectBounds.topRightPoint.x, otherRectBounds.bottomLeftPoint.y);
        if (this.ContainsPoint(cachedPoint))
            return true;
        
        return false;
    }

    public bool ContainsPoint(Vector2 screenPoint) {
        if (screenPoint.x >= this.bottomLeftPoint.x
            && screenPoint.x <= this.topRightPoint.x
            && screenPoint.y >= this.bottomLeftPoint.y
            && screenPoint.y <= this.topRightPoint.y)
            return true;
        return false;
    }

    public override string ToString() {
        return $"Min point: {bottomLeftPoint}, MaxPoint: {topRightPoint}, Size: {size}";
    }
}