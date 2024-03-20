using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Proje_Hastane
{
    public abstract class FormDecorator : IFormComponent
    {
        protected IFormComponent decoratedForm;

        public FormDecorator(IFormComponent form)
        {
            this.decoratedForm = form;
        }

        public virtual void Display()
        {
            decoratedForm.Display();
        }

        public string FormName
        {
            get { return decoratedForm.FormName; }
        }

        public Form GetForm()
        {
            return decoratedForm.GetForm();
        }
    }



}
