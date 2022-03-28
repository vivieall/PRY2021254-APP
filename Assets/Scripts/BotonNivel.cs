using UnityEngine;

class BotonNivel : MonoBehaviour {
    public int id;
    private GameObject rowGameObject;

    private void Start() {
        Debug.Log("OBJECT CREATED WITH ID " + id);
        this.rowGameObject = this.transform.parent.gameObject;
    }

    public GameObject getRowGameObject() {
        return this.gameObject;
    }
}
