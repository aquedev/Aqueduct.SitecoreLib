using System;

namespace Aqueduct.Presentation
{
    public abstract class AbstractPresenter<TView>
        : AbstractPresenter where TView : IAbstractView
    {
        public new TView View
        {
            get { return (TView)base.View; }
            set { base.View = value; }
        }

        protected internal override void CheckViewType(object view)
        {
            Type viewType = view.GetType();
            Type allowedType = typeof(TView);
            if (viewType != allowedType && allowedType.IsAssignableFrom(viewType) == false)
                throw new InvalidOperationException("Object of type " + viewType.Name + " is not allowed as for " + GetType().Name);
        }
    }

    public abstract class AbstractPresenter
    {
        private object m_view;
        public object View
        {
            get { return m_view; }
            set
            {
                CheckViewType(value);
                m_view = value;
            }
        }

        public virtual void Init() { }

        protected internal virtual void CheckViewType(object view) { }
    }
}
