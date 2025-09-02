#region Using

using System.Windows.Forms.Design;

#endregion

namespace AERMOD.LIB.Componentes.Design
{
    public class UserControlDesigner : ParentControlDesigner
    {
        public override void Initialize(System.ComponentModel.IComponent component)
        {
            base.Initialize(component);
           
            if (this.Control is DataLIB)
            {
                this.EnableDesignMode(((DataLIB)this.Control).mtbxZone, "mtbxZone");
                this.EnableDesignMode(((DataLIB)this.Control).ButtonZone, "buttonZone");
            }            
        }
    }
}
