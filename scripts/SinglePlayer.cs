using UnityEngine;
using System.Collections;

//ImaginaryResources Dimensional Hide and Seek
//Saturday, ‎October ‎15, ‎2016, ‏‎9:35:36 AM

public class SinglePlayer : MonoBehaviour {
    public float moveSpeed;
    public GameObject deathParticles;
    private float maxSpeed = 10f;
    private Vector3 input;
    public Transform spawn;
    int camCount = 1;
    int camCountTwo = 1;
    int levelCounter = 1;

    byte r = 255;
    byte g = 255;
    byte b = 255;
    byte a = 255;

    double temp = 0;

    public Material mat;

    MainMenu mainMenu = new MainMenu();

    float pos;


    // Use this for initialization
    void Start() {
        transform.position = spawn.position;

        Camera.main.orthographic = true;
        Camera.main.orthographicSize = 3;
        Camera.main.transform.position = new Vector3(0f, 0.7f, -5.5f);
        Camera.main.transform.rotation = Quaternion.Euler(0, 0, 0);

        mat.color = new Color32(r, g, b, a);
    }

    void FixedUpdate() {
        CameraView();

        if(Input.GetButtonDown("Horizontal")) {
            g--;
            b--;
            mat.color = new Color32 (r, g, b, a);
        }

        if (temp < 1) {
            g++;
            b++;
            mat.color = new Color32(r, g, b, a);
        }


        if (camCount == 1)
            input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        if (camCount == 2)
            input = new Vector3(Input.GetAxisRaw("Vertical"), 0, -Input.GetAxisRaw("Horizontal"));
        if (camCount == 3)
            input = new Vector3(-Input.GetAxisRaw("Horizontal"), 0, -Input.GetAxisRaw("Vertical"));
        if (camCount == 4)
            input = new Vector3(-Input.GetAxisRaw("Vertical"), 0, Input.GetAxisRaw("Horizontal"));

        /*if (camCountTwo == 1)
            input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        if (camCountTwo == 2)
            input = new Vector3(-Input.GetAxisRaw("Vertical"), 0, Input.GetAxisRaw("Horizontal"));
        if (camCountTwo == 3)
            input = new Vector3(-Input.GetAxisRaw("Horizontal"), 0, -Input.GetAxisRaw("Vertical"));
        if (camCountTwo == 4)
            input = new Vector3(Input.GetAxisRaw("Vertical"), 0, -Input.GetAxisRaw("Horizontal"));
        */

        if (GetComponent<Rigidbody>().velocity.magnitude < maxSpeed) {
            GetComponent<Rigidbody>().AddForce(input * moveSpeed);
        }

        if (Input.GetButtonDown("Exit")) {
            mainMenu.LoadLevelSelect();
        }

        if (Input.GetButtonDown("3D")) {
            Camera.main.transform.position = new Vector3(0, 7.5f, -6.2f);
            Camera.main.transform.rotation = Quaternion.Euler(55, 0, 0);
            Camera.main.orthographic = false;
        }
       if (Input.GetButtonUp("3D")){
            Camera.main.orthographic = true;
            Camera.main.orthographicSize = 3;
            Camera.main.transform.position = new Vector3(0, 0.7f, -5.5f);
            Camera.main.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
    public void CameraView() {
        if (Input.GetButtonDown("CameraCW") && camCount == 1) {
            //left
            Camera.main.transform.position = new Vector3(-5.5f, 0.7f, 0);
            Camera.main.transform.rotation = Quaternion.Euler(0, 90, 0);
            GetComponent<Rigidbody>().rotation = Quaternion.Euler(0, 90, 0);
            camCountTwo--;
            camCount++;
        }
        else if (Input.GetButtonDown("CameraCW") && camCount == 2) {
            //opp
            Camera.main.transform.position = new Vector3(0, 0.7f, 5.5f);
            Camera.main.transform.rotation = Quaternion.Euler(0, 180, 0);
            GetComponent<Rigidbody>().rotation = Quaternion.Euler(0, 180, 0);
            camCountTwo--;
            camCount++;
        }
        else if (Input.GetButtonDown("CameraCW") && camCount == 3) {
            //right
            Camera.main.transform.position = new Vector3(5.5f, 0.7f, 0);
            Camera.main.transform.rotation = Quaternion.Euler(0, -90, 0);
            GetComponent<Rigidbody>().rotation = Quaternion.Euler(0, -90, 0);
            camCountTwo--;
            camCount++;
        }
        else if (Input.GetButtonDown("CameraCW") && camCount == 4) {
            //front
            Camera.main.transform.position = new Vector3(0, 0.7f, -5.5f);
            Camera.main.transform.rotation = Quaternion.Euler(0, 0, 0);
            GetComponent<Rigidbody>().rotation = Quaternion.Euler(0, 0, 0);
            camCountTwo--;
            camCount = 1;
        }
    }

    void OnCollisionEnter(Collision other) {
        if (other.transform.tag == "Hider") {
            Instantiate(deathParticles, transform.position, Quaternion.Euler(270, 0, 0));
            Destroy(GameObject.FindWithTag("Hider"));
            levelCounter++;
        }
    }

    IEnumerator waitSeconds() {
        yield return new WaitForSeconds(3);
    }
}
