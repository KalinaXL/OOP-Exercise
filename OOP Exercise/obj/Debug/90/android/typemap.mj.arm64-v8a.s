	/* Data SHA1: a89010c34aa213d13293d47c0dfb1dc51b813685 */
	.arch	armv8-a
	.file	"typemap.mj.inc"

	/* Mapping header */
	.section	.data.mj_typemap,"aw",@progbits
	.type	mj_typemap_header, @object
	.p2align	2
	.global	mj_typemap_header
mj_typemap_header:
	/* version */
	.word	1
	/* entry-count */
	.word	1170
	/* entry-length */
	.word	262
	/* value-offset */
	.word	145
	.size	mj_typemap_header, 16

	/* Mapping data */
	.type	mj_typemap, @object
	.global	mj_typemap
mj_typemap:
	.size	mj_typemap, 306541
	.include	"typemap.mj.inc"
