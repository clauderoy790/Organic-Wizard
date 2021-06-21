using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Organic_Wizard
{
    public class GameObject
    {
        List<Component> _components = null;
        private bool _active = false;

        public bool Active
        {
            get { return _active; }
            set
            {
                _active = value;
                _components.ForEach(c => { c.Active = value; });
            }
        }

        public GameObject()
        {
            _components = new List<Component>();
        }

        public void AddComponent(Component comp)
        {
            if (comp != null)
            {
                comp.SetGameObject(this);
                _components.Add(comp);
                if (Active && comp.Active)
                    comp.OnEnable();
            }
        }

        public T GetComponent<T>() where T : Component{
            T component = null;

            foreach(var comp in _components)
            {
                if (comp is T)
                {
                    component = (T)comp;
                    break;
                }
            }

            return component;
        }

        public void Update()
        {
            if (!Active)
                return;

            for (int i = 0; i < _components.Count; i++)
            {
                if (_components[i] != null)
                {
                    if (_components[i].Active)
                        _components[i].OnUpdate();
                }
                else
                {
                    _components.RemoveAt(i--);
                }
            }
        }
    }
}
