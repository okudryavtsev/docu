using System.Collections.Generic;
using System.Linq;
using Docu.Documentation.Comments;
using Docu.Parsing.Model;

namespace Docu.Documentation
{
    public abstract class BaseDocumentationElement : IDocumentationElement
    {
        protected readonly Identifier identifier;

        protected BaseDocumentationElement(Identifier identifier)
        {
            Name = identifier.ToString();
            this.identifier = identifier;
            IsResolved = true;
            Summary = new Summary();
            Remarks = new Remarks();
            Value = new Value();
            Example = new MultilineCode();
        }

        public virtual bool HasDocumentation
        {
            get { return !(Summary.IsEmpty && Remarks.IsEmpty && Value.IsEmpty); }
        }

        public string Name { get; private set; }
        public bool IsExternal { get; private set; }
        public bool IsResolved { get; protected set; }
        public Summary Summary { get; set; }
        public Remarks Remarks { get; set; }
        public Value Value { get; set; }
        public MultilineCode Example { get; set; }

        public bool IsIdentifiedBy(Identifier otherIdentifier)
        {
            return identifier.Equals(otherIdentifier);
        }

        public virtual void ConvertToExternalReference()
        {
            IsExternal = true;
        }
    }
}