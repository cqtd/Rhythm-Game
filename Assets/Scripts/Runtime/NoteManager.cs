using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Cqunity.Rhythmical
{
    public class NoteManager : MonoBehaviour
    {
        [SerializeField] protected int bpm = default;
        private double m_currentTime = 0d;

        [SerializeField] private Transform tfNoteAppear = default;
        [SerializeField] private Note notePrefab = default;

        [SerializeField] private Transform m_canvas = default;

        [SerializeField] private Lane[] m_lanes;
    
        // Start is called before the first frame update
        private void Start()
        {
        
        }

        // Update is called once per frame
        private void Update()
        {
            m_currentTime += Time.deltaTime;

            if (m_currentTime >= 60d / bpm)
            {
                foreach (Lane lane in m_lanes)
                {
                    bool spawn = Random.Range(0, 5) > 2;
                    if (spawn)
                    {
                        Note note = Instantiate(notePrefab, lane.spawnPosition, Quaternion.identity, m_canvas);
                        lane.Add(note);
                    }
                }
                
                m_currentTime -= 60d / bpm;                        
            }
        }
    }
}
