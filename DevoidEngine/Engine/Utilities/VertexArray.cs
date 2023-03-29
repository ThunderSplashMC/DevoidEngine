﻿using System;
using System.Collections.Generic;

using OpenTK.Graphics.OpenGL;

namespace DevoidEngine.Engine.Utilities
{
    public class VertexArray : IDisposable
    {

        public readonly int VertexArrayObject;
        public readonly VertexBuffer VertexBuffer;
        //public readonly IndexBuffer IndexBuffer;

        private bool isdisposed = false;
        private bool isinitialized = false;

        public VertexArray(VertexBuffer vertexBuffer)
        {
            this.isdisposed = false;
            this.isinitialized = true;

            if (vertexBuffer is null)
            {
                throw new ArgumentException();
            }

            this.VertexBuffer = vertexBuffer;
            //this.IndexBuffer = indexBuffer;
            int VertexSizeInBytes = this.VertexBuffer.VertexInfo.SizeInBytes;

            VertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(VertexArrayObject);

            GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBuffer.VertexBufferObject);

            for (int i = 0; i < VertexBuffer.VertexInfo.VertexAttributes.Length; i++)
            {
                VertexAttribute attribute = VertexBuffer.VertexInfo.VertexAttributes[i];
                GL.VertexAttribPointer(attribute.Index, attribute.ComponentCount, VertexAttribPointerType.Float, false, VertexSizeInBytes, attribute.Offset);
                GL.EnableVertexAttribArray(attribute.Index);
            }

            GL.BindVertexArray(0);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.DeleteBuffer(vertexBuffer.VertexBufferObject);
        }

        public void Bind()
        {
            GL.BindVertexArray(VertexArrayObject);
        }

        public void Render()
        {
            GL.BindVertexArray(VertexArrayObject);
            //GL.BindBuffer(BufferTarget.ElementArrayBuffer, IndexBuffer.IndexBufferObject);
            //GL.DrawElements(PrimitiveType.Triangles, IndexBuffer.IndexCount, DrawElementsType.UnsignedInt, 0);

            GL.DrawArrays(PrimitiveType.Triangles, 0, VertexBuffer.VertexCount);//VertexBuffer.VertexCount);
        }

        public void RenderWithIndices(IndexBuffer IBO)
        {
            GL.BindVertexArray(VertexArrayObject);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, IBO.IndexBufferObject);
            GL.DrawElements(PrimitiveType.Triangles, IBO.IndexCount, DrawElementsType.UnsignedInt, 0);
        }

        ~VertexArray()
        {
            this.Dispose();
        }

        public void Dispose()
        {
            if (isdisposed)
            {
                return;
            }
            VertexBuffer.Dispose();
            OpenGLObjectManager.AddVAOToDispose(VertexArrayObject);

            this.isdisposed = true;

            GC.SuppressFinalize(this);
        }
    }
}
