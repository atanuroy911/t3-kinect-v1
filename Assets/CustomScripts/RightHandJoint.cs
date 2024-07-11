using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class RightHandJoint : MonoBehaviour
{
    // the joint we want to track
    public KinectWrapper.NuiSkeletonPositionIndex RHjoint = KinectWrapper.NuiSkeletonPositionIndex.HandRight;
    public KinectWrapper.NuiSkeletonPositionIndex RWjoint = KinectWrapper.NuiSkeletonPositionIndex.WristRight;
    public KinectWrapper.NuiSkeletonPositionIndex REjoint = KinectWrapper.NuiSkeletonPositionIndex.ElbowRight;
    public KinectWrapper.NuiSkeletonPositionIndex RSjoint = KinectWrapper.NuiSkeletonPositionIndex.ShoulderRight;

    public Text RH_X;
    public Text RH_Y;
    public Text RH_Z;
    public Text RW_X;
    public Text RW_Y;
    public Text RW_Z;
    public Text RE_X;
    public Text RE_Y;
    public Text RE_Z;
    public Text RS_X;
    public Text RS_Y;
    public Text RS_Z;

    Button loggingStartStop;
    Text loggingStartStopText;


    // joint position at the moment, in Kinect coordinates
    public Vector3 outputPosition;

    // if it is saving data to a csv file or not
    public bool isSaving = false;

    // how many seconds to save data into the csv file, or 0 to save non-stop
    public float secondsToSave = 0f;

    // path to the csv file (;-limited)
    public string RHsaveFilePath = "RH_joint_pos.csv";
    public string RWsaveFilePath = "RW_joint_pos.csv";
    public string REsaveFilePath = "RE_joint_pos.csv";
    public string RSsaveFilePath = "RS_joint_pos.csv";


    // start time of data saving to csv file
    private float saveStartTime = -1f;

    private void Start()
    {
        RH_X = GameObject.Find("RH_X-Axis").GetComponent<Text>();
        RH_Y = GameObject.Find("RH_Y-Axis").GetComponent<Text>();
        RH_Z = GameObject.Find("RH_Z-Axis").GetComponent<Text>();
        RW_X = GameObject.Find("RW_X-Axis").GetComponent<Text>();
        RW_Y = GameObject.Find("RW_Y-Axis").GetComponent<Text>();
        RW_Z = GameObject.Find("RW_Z-Axis").GetComponent<Text>();
        RE_X = GameObject.Find("RE_X-Axis").GetComponent<Text>();
        RE_Y = GameObject.Find("RE_Y-Axis").GetComponent<Text>();
        RE_Z = GameObject.Find("RE_Z-Axis").GetComponent<Text>();
        RS_X = GameObject.Find("RS_X-Axis").GetComponent<Text>();
        RS_Y = GameObject.Find("RS_Y-Axis").GetComponent<Text>();
        RS_Z = GameObject.Find("RS_Z-Axis").GetComponent<Text>();

        loggingStartStop = GameObject.Find("startStopLoggingButton").GetComponent<Button>();
        loggingStartStopText = GameObject.Find("startStopLoggingButton").GetComponentInChildren<Text>();

        if (loggingStartStop != null)
            loggingStartStop.onClick.AddListener(buttonTask);
        
    }

    private void buttonTask()
    {
        isSaving = !isSaving;
        if (isSaving)
        {
            loggingStartStopText.text = "Stop Logging";
        }
        if (!isSaving)
        {
            loggingStartStopText.text = "Start Logging";
        }
        throw new NotImplementedException();
    }

    void Update()
    {
        if (isSaving)
        {
            // create the file, if needed
            if (!File.Exists(RHsaveFilePath))
            {
                using (StreamWriter writer = File.CreateText(RHsaveFilePath))
                {
                    // csv file header
                    string sLine = "time;joint;pos_x;pos_y;poz_z";
                    writer.WriteLine(sLine);
                }
            }
            if (!File.Exists(RWsaveFilePath))
            {
                using (StreamWriter writer = File.CreateText(RWsaveFilePath))
                {
                    // csv file header
                    string sLine = "time;joint;pos_x;pos_y;poz_z";
                    writer.WriteLine(sLine);
                }
            }
            if (!File.Exists(REsaveFilePath))
            {
                using (StreamWriter writer = File.CreateText(REsaveFilePath))
                {
                    // csv file header
                    string sLine = "time;joint;pos_x;pos_y;poz_z";
                    writer.WriteLine(sLine);
                }
            }
            if (!File.Exists(RSsaveFilePath))
            {
                using (StreamWriter writer = File.CreateText(RSsaveFilePath))
                {
                    // csv file header
                    string sLine = "time;joint;pos_x;pos_y;poz_z";
                    writer.WriteLine(sLine);
                }
            }

            // check the start time
            if (saveStartTime < 0f)
            {
                saveStartTime = Time.time;
            }
        }

        // get the joint position
        KinectManager manager = KinectManager.Instance;

        if (manager && manager.IsInitialized())
        {
            if (manager.IsUserDetected())
            {
                uint userId = manager.GetPlayer1ID();

                if (manager.IsJointTracked(userId, (int)RHjoint))
                {
                    // output the joint position for easy tracking
                    Vector3 RH_jointPos = manager.GetJointPosition(userId, (int)RHjoint);
                    outputPosition = RH_jointPos;
                    RH_X.text = string.Format("{0:F3}", RH_jointPos.x);
                    //Debug.Log(RH_jointPos.x);
                    RH_Y.text = string.Format("{0:F3}", RH_jointPos.y);
                    RH_Z.text = string.Format("{0:F3}", RH_jointPos.z);



                    if (isSaving)
                    {
                        if ((secondsToSave == 0f) || ((Time.time - saveStartTime) <= secondsToSave))
                        {
                            using (StreamWriter writer = File.AppendText(RHsaveFilePath))
                            {
                                string sLine = string.Format("{0:F3};{1};{2:F3};{3:F3};{4:F3}", Time.time, (int)RHjoint, RH_jointPos.x, RH_jointPos.y, RH_jointPos.z);
                                writer.WriteLine(sLine);
                            }
                        }
                    }
                }
                if (manager.IsJointTracked(userId, (int)RWjoint))
                {
                    // output the joint position for easy tracking
                    Vector3 RW_jointPos = manager.GetJointPosition(userId, (int)RWjoint);
                    outputPosition = RW_jointPos;
                    RW_X.text = string.Format("{0:F3}", RW_jointPos.x);
                    //Debug.Log(RH_jointPos.x);
                    RW_Y.text = string.Format("{0:F3}", RW_jointPos.y);
                    RW_Z.text = string.Format("{0:F3}", RW_jointPos.z);

                    if (isSaving)
                    {
                        if ((secondsToSave == 0f) || ((Time.time - saveStartTime) <= secondsToSave))
                        {
                            using (StreamWriter writer = File.AppendText(RWsaveFilePath))
                            {
                                string sLine = string.Format("{0:F3};{1};{2:F3};{3:F3};{4:F3}", Time.time, (int)RHjoint, RW_jointPos.x, RW_jointPos.y, RW_jointPos.z);
                                writer.WriteLine(sLine);
                            }
                        }
                    }
                }
                if (manager.IsJointTracked(userId, (int)REjoint))
                {
                    // output the joint position for easy tracking
                    Vector3 RE_jointPos = manager.GetJointPosition(userId, (int)REjoint);
                    outputPosition = RE_jointPos;
                    RE_X.text = string.Format("{0:F3}", RE_jointPos.x);
                    //Debug.Log(RH_jointPos.x);
                    RE_Y.text = string.Format("{0:F3}", RE_jointPos.y);
                    RE_Z.text = string.Format("{0:F3}", RE_jointPos.z);

                    if (isSaving)
                    {
                        if ((secondsToSave == 0f) || ((Time.time - saveStartTime) <= secondsToSave))
                        {
                            using (StreamWriter writer = File.AppendText(REsaveFilePath))
                            {
                                string sLine = string.Format("{0:F3};{1};{2:F3};{3:F3};{4:F3}", Time.time, (int)RHjoint, RE_jointPos.x, RE_jointPos.y, RE_jointPos.z);
                                writer.WriteLine(sLine);
                            }
                        }
                    }
                }
                if (manager.IsJointTracked(userId, (int)RSjoint))
                {
                    // output the joint position for easy tracking
                    Vector3 RS_jointPos = manager.GetJointPosition(userId, (int)RSjoint);
                    outputPosition = RS_jointPos;
                    RS_X.text = string.Format("{0:F3}", RS_jointPos.x);
                    //Debug.Log(RH_jointPos.x);
                    RS_Y.text = string.Format("{0:F3}", RS_jointPos.y);
                    RS_Z.text = string.Format("{0:F3}", RS_jointPos.z);

                    if (isSaving)
                    {
                        if ((secondsToSave == 0f) || ((Time.time - saveStartTime) <= secondsToSave))
                        {
                            using (StreamWriter writer = File.AppendText(RSsaveFilePath))
                            {
                                string sLine = string.Format("{0:F3};{1};{2:F3};{3:F3};{4:F3}", Time.time, (int)RHjoint, RS_jointPos.x, RS_jointPos.y, RS_jointPos.z);
                                writer.WriteLine(sLine);
                            }
                        }
                    }
                }
            }
        }

    }

}
