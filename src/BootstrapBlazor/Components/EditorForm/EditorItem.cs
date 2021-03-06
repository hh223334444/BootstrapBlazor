﻿using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// EditorItem 组件
    /// </summary>
    /// <remarks>用于 EditorForm 的 FieldItems 模板内</remarks>
    public class EditorItem<TValue> : ComponentBase, IEditorItem
    {
#nullable disable
        /// <summary>
        /// 获得/设置 绑定字段值
        /// </summary>
        [Parameter]
        public TValue Field { get; set; }
#nullable restore

        /// <summary>
        /// 获得/设置 绑定字段值变化回调委托
        /// </summary>
        [Parameter]
        public EventCallback<TValue> FieldChanged { get; set; }

        /// <summary>
        /// 获得/设置 绑定列类型
        /// </summary>
        [NotNull]
        public Type? FieldType { get; set; }

        /// <summary>
        /// 获得/设置 ValueExpression 表达式
        /// </summary>
        [Parameter]
        public Expression<Func<TValue>>? FieldExpression { get; set; }

        /// <summary>
        /// 获得/设置 当前列是否可编辑 默认为 true 当设置为 false 时自动生成编辑 UI 不生成此列
        /// </summary>
        [Parameter]
        public bool Editable { get; set; } = true;

        /// <summary>
        /// 获得/设置 当前列编辑时是否为只读模式 默认为 false
        /// </summary>
        [Parameter]
        public bool Readonly { get; set; }

        /// <summary>
        /// 获得/设置 表头显示文字
        /// </summary>
        [Parameter]
        public string? Text { get; set; }

        /// <summary>
        /// 获得/设置 编辑模板
        /// </summary>
        [Parameter]
        public RenderFragment<object>? EditTemplate { get; set; }

        /// <summary>
        /// 获得/设置 IEditorItem 集合实例
        /// </summary>
        /// <remarks>EditorForm 组件级联传参下来的值</remarks>
        [CascadingParameter]
        private List<IEditorItem>? EditorItems { get; set; }

        /// <summary>
        /// OnInitialized 方法
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();

            EditorItems?.Add(this);
            if (FieldExpression != null) _fieldIdentifier = FieldIdentifier.Create(FieldExpression);

            // 获取模型属性定义类型
            FieldType = typeof(TValue);
        }

        private FieldIdentifier? _fieldIdentifier;
        /// <summary>
        /// 获取绑定字段显示名称方法
        /// </summary>
        public string GetDisplayName() => Text ?? _fieldIdentifier?.GetDisplayName() ?? "";

        /// <summary>
        /// 获取绑定字段信息方法
        /// </summary>
        public string GetFieldName() => _fieldIdentifier?.FieldName ?? "";
    }
}
