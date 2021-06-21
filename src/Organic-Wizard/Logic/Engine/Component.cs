using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Organic_Wizard
{
    public abstract class Component
    {
        private bool _active = true;

        public GameObject GameObject { get; private set; }

        public Component()
        {

        }

        public Component(GameObject go)
        {
            SetGameObject(go);
        }

        public bool Active
        {
            get { return _active; }
            set
            {
                _active = value;

                if (_active)
                    OnEnable();
                else
                    OnDisable();
            }
        }

        public T GetComponent<T>() where T : Component{
            if (GameObject == null)
                return null;

            return GameObject.GetComponent<T>();
        }

        public void SetGameObject(GameObject go)
        {
            if (go != null)
            {
                GameObject beforeInit = GameObject;
                GameObject = go;
                if (beforeInit != GameObject)
                    this.onAttach();
            }
        }

        public virtual void onAttach()
        {

        }

        public virtual void OnEnable()
        {

        }

        public virtual void OnDisable()
        {

        }

        public virtual void OnUpdate()
        {

        }
    }
}
